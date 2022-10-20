using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ASO_VR
{
    public class SaveLoad : MonoBehaviour
    {
        [SerializeField] protected StatesList _loadStates;
        [SerializeField] protected ObjectsList _objectsList;

        private readonly string _reportsFolder = "Report";
        private readonly string _statesFolder = "States";
        
        private string _directoryPath;// = Application.dataPath;

        protected void Start()
        {
            if (!_loadStates.GetLoadState())
            {
                LoadAllStates();
                LoadAllReports();
            }
        }

        private void OnApplicationQuit()
        {
#if UNITY_EDITOR
            _loadStates.ClearAfterTest();
#endif
        }

        private void LoadAllStates()
        {
            string DirPass = Application.dataPath;
            string path = Path.Combine(DirPass, _statesFolder);

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path).Select(Path.GetFileNameWithoutExtension).Distinct();

                foreach (var file in files)
                {
                    string fileName = file.Replace(path + @"\", "");
                    int num;

                    if (int.TryParse(fileName,out num))
                    {
                        StreamReader sr = new StreamReader(Path.Combine(path, file));
                        JSONObject jObj = new JSONObject(sr.ReadToEnd());
                        sr.Close();

                        Lesson newLesson = new Lesson(_objectsList.GetTypeObjectsList());
                        newLesson.Load(jObj);
                    
                        _loadStates.AddState(jObj.GetField("Lesson").GetField("Name").str, newLesson);
                        _loadStates.AddStateName(jObj.GetField("Lesson").GetField("Name").str, Int32.Parse(fileName));
                    }
                }

                if (_loadStates.GetStates().Count > 0)
                {
                    _loadStates.SetLoadState(true);
                }
            }
        }
        
        private void LoadAllReports()
        {
            string DirPass = Application.dataPath;
            string path = Path.Combine(DirPass, _reportsFolder);

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path).Select(Path.GetFileNameWithoutExtension).Distinct();

                foreach (var file in files)
                {
                    string fileName = file.Replace(path + @"\", "");
                    int num;

                    if (int.TryParse(fileName,out num))
                    {
                        StreamReader sr = new StreamReader(Path.Combine(path, file));
                        JSONObject jObj = new JSONObject(sr.ReadToEnd());
                        sr.Close();

                        Report newReport = new Report();
                        newReport.Load(jObj);
                    
                        _loadStates.AddReport(newReport.GetName(), newReport);
                        _loadStates.AddReportName(newReport.GetName(), num);
                    }
                }
            }
        }

        protected void DeleteStateByName(string name, TypeSave typeSave)
        {
            string DirPass = Application.dataPath;
            string nameFile = typeSave == TypeSave.States
                ? _loadStates.GetDictionaryStateNames()[name].ToString()
                : _loadStates.GetDictionaryReportNames()[name].ToString();
            
            string path = Path.Combine(DirPass, GetFolderByType(typeSave), nameFile);

            if (File.Exists(path))
            {
                File.Delete(path);
                switch (typeSave)
                {
                    case TypeSave.States : _loadStates.DeleteStateByName(name); break;
                    case TypeSave.Reports : _loadStates.DeleteReportByName(name); break;
                }
            }

            path = path + ".meta";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public bool TrySave(string name, string comment, TypeSave typeSave)
        {
            string DirPass = Application.dataPath;
            string path = Path.Combine(DirPass, GetFolderByType(typeSave));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (VerificationNameInDictionary(name, typeSave))
            {
                return false;
            }
            
            JSONObject jObj = new JSONObject();

            Lesson newLesson;
            if (typeSave == TypeSave.Reports)
            {
                newLesson = _loadStates.GetCurrentLesson();
                Report newReport = new Report(newLesson.Save(), name, comment);
                jObj = newReport.Save();
                _loadStates.AddReport(name, newReport);
            }
            else
            {
                newLesson = new Lesson(_objectsList.GetTypeObjectsList());
                newLesson.SetName(name);
                newLesson.SetComment(comment);
                _loadStates.AddState(name, newLesson);
                jObj = newLesson.Save();
            }
            
            string number = GetNumberName(typeSave);
            path = Path.Combine(path, number);

            switch (typeSave)
            {
                case TypeSave.States: _loadStates.AddStateName(name, int.Parse(number)); break;
                case TypeSave.Reports: _loadStates.AddReportName(name, int.Parse(number)); break;
            }
            
            StreamWriter sw = new StreamWriter(path);
            sw.Write(jObj.ToString());
            sw.Close();
            
            return true;
        }

        private bool VerificationNameInDictionary(string name, TypeSave typeSave)
        {
            return typeSave == TypeSave.States
                ? _loadStates.GetStates().ContainsKey(name)
                : _loadStates.GetReports().ContainsKey(name);
        }

        private string GetNumberName(TypeSave typeSave)
        {
            int number = 0;

            Dictionary<string, int> names = typeSave == TypeSave.States
                ? _loadStates.GetDictionaryStateNames()
                : _loadStates.GetDictionaryReportNames();

            while (names.ContainsValue(number))
            {
                number++;
            }

            return number.ToString();
        }

        protected void LoadStateByName(string name)
        {
            _objectsList.SetTypeObjectsList(_loadStates.GetLoadDataByName(name).GetListObjectTypes());
        }

        private string GetFolderByType(TypeSave typeSave)
        {
            string pathFolder = "";
            
            switch (typeSave)
            {
                case TypeSave.Reports: pathFolder = _reportsFolder; break;
                case TypeSave.States: pathFolder = _statesFolder; break;
            }
            
            return pathFolder;
        }

        protected void SetCurrenrState(string name)
        {
            _loadStates.SetCurrentLesson(name);
        }

    }

    public enum TypeSave
    {
        Reports, States
    }
}
