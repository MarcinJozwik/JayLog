using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace JayTools.JayLogs.Editor
{
    /// <summary>
    /// Sets the preferences of the JayLog logging system. Includes changing the filter options,
    /// managing saving settings and altering define symbols. 
    /// </summary>
    public class LogEditorWindow : EditorWindow
    {
        private static string[] priorities;
        private static string[] categories;

        private static string priorityPrint;
        private static string categoryPrint;

        private static int visiblePriorityMask;
        private static int visibleCategoryMask;
        
        private static int appliedPriorityMask;
        private static int appliedCategoryMask;

        private static bool clearLogFileOnStart;

        private const string CategoryString = "Active categories: ";
        private const string PriorityString = "Active priorities: ";

        private bool saveFoldout;
        
        private readonly string [] defineSymbols = {"ENABLE_LOG_FILE"};

        [MenuItem("Tools/JayTools/Jay Log Window")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(LogEditorWindow));
            window.titleContent = new GUIContent("Jay Log");
            window.minSize = new Vector2(300,200);
        }

        private void OnEnable()
        {
            categories = Enum.GetNames(typeof(LogCategory)).ToArray();
            priorities = Enum.GetNames(typeof(LogPriority)).ToArray();

            appliedCategoryMask = PlayerPrefs.GetInt(JayLogService.LogCategorySaveKey, 0);
            appliedPriorityMask = PlayerPrefs.GetInt(JayLogService.LogPrioritySaveKey, 0);
            clearLogFileOnStart = PlayerPrefs.GetInt(JayLogService.ClearLogOnStartKey, 1) == 1;

            categoryPrint = CategoryString + GetCurrentMask(appliedCategoryMask, categories);
            priorityPrint = PriorityString + GetCurrentMask(appliedPriorityMask, priorities);

            visibleCategoryMask = appliedCategoryMask;
            visiblePriorityMask = appliedPriorityMask;
        }

        private void OnGUI()
        {
            DrawOptions("Priority", PriorityString, ref appliedPriorityMask, ref visiblePriorityMask, ref priorityPrint, JayLogService.LogPrioritySaveKey, priorities);
            EditorGUILayout.Space();
            DrawOptions("Category", CategoryString, ref appliedCategoryMask, ref visibleCategoryMask, ref categoryPrint, JayLogService.LogCategorySaveKey, categories);
            DrawLogFileSettings();
        }
        
        private void DrawOptions(string header, string description, ref int debugMask, ref int newMask, ref string maskPrint, string saveKey, string[] options)
        {
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(maskPrint, MessageType.Info);
            if (newMask != debugMask)
            {
                EditorGUILayout.HelpBox("Unapplied changes detected", MessageType.Warning); 
            }
            EditorGUILayout.Space();
            
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            
            newMask = EditorGUILayout.MaskField("New " + header.ToLower(), newMask, options);

            EditorGUILayout.Space();
            if (GUILayout.Button("Apply"))
            {
                debugMask = newMask;
                maskPrint = description + GetCurrentMask(debugMask, options);
                
                PlayerPrefs.SetInt(saveKey, debugMask);
                
                Debug.Log($"New {header.ToLower()} mask applied.\n{maskPrint} ({newMask})");
            }
            
            EditorGUI.EndDisabledGroup();
            
            GUILayout.EndVertical();
        }

        private void DrawLogFileSettings()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Log File Settings", EditorStyles.boldLabel);

            saveFoldout = EditorGUILayout.Foldout(saveFoldout, "Saving Log File");
            
            //foldout here to avoid the constant update of the define symbol list in the button logic
            if (saveFoldout)
            {
                bool showDisableButton = HasAllSymbols();

                if (showDisableButton)
                {
                    if (GUILayout.Button("Disable Saving Log File"))
                    {
                        RemoveDefineSymbols();
                    }
                }
                else
                {
                    if (GUILayout.Button("Enable Saving Log File"))
                    {
                        AddDefineSymbols();
                    }
                }
            }
           
            EditorGUI.BeginChangeCheck();
            clearLogFileOnStart = EditorGUILayout.Toggle("Clear Log File On Start", clearLogFileOnStart);
            if (EditorGUI.EndChangeCheck())
            {
                PlayerPrefs.SetInt(JayLogService.ClearLogOnStartKey, clearLogFileOnStart ? 1 : 0);
            }
        }
        
        private void AddDefineSymbols()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
            
            List<string> allDefines = definesString.Split ( ';' ).ToList ();
            allDefines.AddRange ( defineSymbols.Except ( allDefines ) );
            
            PlayerSettings.SetScriptingDefineSymbolsForGroup (
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join ( ";", allDefines.ToArray () ) );
        }
        
        private void RemoveDefineSymbols()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
            
            List<string> allDefines = definesString.Split ( ';' ).ToList ();
            
            foreach (string symbol in defineSymbols)
            {
                allDefines.Remove(symbol);
            }
            
            PlayerSettings.SetScriptingDefineSymbolsForGroup (
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join ( ";", allDefines.ToArray () ) );
        }

        private bool HasAllSymbols()
        {
            // constant update is required to ensure up-to-date list of define symbols
            // user can modify the list manually when the Inspector Window is on,
            // what can lead to some inconsistency
            
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
            List<string> allDefines = definesString.Split ( ';' ).ToList ();
            
            foreach (string symbol in defineSymbols)
            {
                if (!allDefines.Contains(symbol))
                {
                    return false;
                }
            }

            return true;
        }

        private static string GetCurrentMask(int mask, string[] options)
        {
            StringBuilder builder = new StringBuilder();
            if (mask == 0)
            {
                builder.Append("nothing");
                return builder.ToString();
            }

            if (mask == -1)
            {
                builder.Append("everything");
                return builder.ToString();
            }
            
            for (int i = 0; i < options.Length; i++)
            {
                int value = 1 << i;
                if ((value & mask) == value)
                {
                    builder.Append(options[i] + " | ");
                }
            }

            string printedMask = builder.ToString();
            return printedMask.Substring(0, printedMask.Length - 2);
        }
    }
}