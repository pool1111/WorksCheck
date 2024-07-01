using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WorksCheck.Scripts
{
    public class HomeworkData
    {
        private SubjectData? _subject = null;
        private string _subjectname = string.Empty;
        private string _content = string.Empty;
        private bool _isachieve = false;
        private bool _isinform = false;
        private DateTime? _date = null;
        private SolidColorBrush _color = new SolidColorBrush(Colors.White);
        private Priority? _priority = null;
        private string _remark = string.Empty;

        public SubjectData? Subject { 
            get { return _subject; }
            set {
                _subject = value;
                SetColor();
                SetSubjectName();
            } 
        }
        public string SubjectName { get => _subjectname; }
        public string Content { get => _content; set => _content = value; }
        public bool IsAchieve { get => _isachieve; set => _isachieve = value; }
        public bool IsInform { get => _isinform; set => _isinform = value; }
        public DateTime? Date { get=>_date; set => _date = value; }

        //制限時間・開始時間は未実装
        public SolidColorBrush Color { get => _color; }
        public Priority? Priority { get=>_priority; set => _priority = value; }
        public string Remark { get=>_remark; set => _remark = value; }


        private void SetColor()
        {
            switch (_subject)
            {
                case SubjectData.Japanese:
                    _color = App.Current.Resources["JapaneseColor"] as SolidColorBrush;
                    return;

                case SubjectData.Math:
                    _color = App.Current.Resources["MathColor"] as SolidColorBrush;
                    return;

                case SubjectData.English:
                    _color = App.Current.Resources["EnglishColor"] as SolidColorBrush;
                    return;

                case SubjectData.Social:
                    _color = App.Current.Resources["SocialColor"] as SolidColorBrush;
                    return;

                case SubjectData.Science:
                    _color = App.Current.Resources["ScienceColor"] as SolidColorBrush;
                    return;
            }

            if(_subject == SubjectData.HomeEco || _subject == SubjectData.Info ||  _subject == SubjectData.PE || _subject == SubjectData.Music || _subject == SubjectData.Art || _subject == SubjectData.CallG)
            {
                _color = App.Current.Resources["SubColor"] as SolidColorBrush;
                return;
            }
            MessageBox.Show("_colorに色を代入できませんでした。","Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SetSubjectName()
        {
            switch (_subject)
            {
                case SubjectData.Japanese:
                    _subjectname = "国語";
                    return;

                case SubjectData.Math:
                    _subjectname = "数学";
                    return;

                case SubjectData.English:
                    _subjectname = "英語";
                    return;

                case SubjectData.Social:
                    _subjectname = "社会";
                    return;

                case SubjectData.Science:
                    _subjectname = "理科";
                    return;
                case SubjectData.HomeEco:
                    _subjectname = "家庭";
                    return;
                case SubjectData.Info:
                    _subjectname = "情報";
                    return;
                case SubjectData.PE:
                    _subjectname = "保体";
                    return;
                case SubjectData.Music:
                    _subjectname = "音楽";
                    break;
                case SubjectData.Art:
                    _subjectname = "美術";
                    return;
                case SubjectData.CallG:
                    _subjectname = "書写";
                    return;
            }
        }
    }

    public enum SubjectData
    {
        Japanese,
        Math,
        English,
        Social,
        Science,
        HomeEco,
        Info,
        PE,
        Music,
        Art,
        CallG,
    }

    public enum Priority
    {
        高,
        中,
        低,
    }
}
