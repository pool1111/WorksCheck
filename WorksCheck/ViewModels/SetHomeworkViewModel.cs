using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WorksCheck.Resources.Settings;
using WorksCheck.Resources.UserControls;
using WorksCheck.Scripts;
using WorksCheck.Views;

namespace WorksCheck.ViewModels
{
    public class SetHomeworkViewModel : ViewModelBase
    {
        /// <param name="homeworkData">すでに書き込まれているHomeworkData</param>
        public SetHomeworkViewModel(HomeworkData homeworkData)
        {
            SubjectName = homeworkData.SubjectName;
            SubjectData = homeworkData.Subject;
            Content = homeworkData.Content;
            IsInform = homeworkData.IsInform;
            Date = homeworkData.Date;
            Color = homeworkData.Color;
            Priority = homeworkData.Priority;
            Remark = homeworkData.Remark;

            ReformMode = true;
            initList();

            SaveDataCommand = CreateCommand(v =>
            {
                if(MessageBox.Show("保存しますか？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    UserSetting.Default.Plan.Remove(homeworkData);
                    HomeworkData homeworkdata = new()
                    {
                        Subject = SubjectData,
                        Content = Content,
                        IsInform = IsInform,
                        Date = Date,
                        Priority = Priority,
                        Remark = Remark
                    };
                    UserSetting.Default.Plan.Add(homeworkdata);
                    UserSetting.Default.Save();
                    View.Close();
                    DayRack.LoadHomeworkData();
                }
                else { return; }
            }, null);

        }

        public SetHomeworkViewModel()
        {
            initList();
            ReformMode = false;

            SaveDataCommand = CreateCommand(v =>
            {
                if (MessageBox.Show("保存しますか？", "注意", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HomeworkData homeworkdata = new()
                    {
                        Subject = SubjectData,
                        Content = Content,
                        IsInform = IsInform,
                        Date = Date,
                        Priority = Priority,
                        Remark = Remark
                    };
                    //MessageBox.Show("content." + Content + " date." + Date + " priority." + Priority + " remark." + Remark + " isinform." + IsInform);
                    UserSetting.Default.Plan.Add(homeworkdata);
                    UserSetting.Default.Save();
                    View.Close();
                    DayRack.LoadHomeworkData();
                }else {  return; }

            }, null);
        }

        private HomeworkData forReformMode;

        public SetHomeworkWindow View { get; set; }

        public DayRack DayRack { get; set; }

        private ObservableCollection<PriorityList> _priorityLists;
        public ObservableCollection<PriorityList> PriorityLists
        {
            get=> _priorityLists;
            set
            {
                _priorityLists = value;
                OnPropertyChanged();
            }
        }

        private int _priorityselectedindex = 1;
        public int PrioritySelectedIndex
        {
            get => _priorityselectedindex; 
            set
            {
                _priorityselectedindex = value;
                OnPropertyChanged();
            }
        }

        private bool ReformMode = false;

        //HomeworkData用のプロパティ
        private string _subjectname = string.Empty;
        public string SubjectName
        {
            get => _subjectname;
            set
            {
                _subjectname = value;
                OnPropertyChanged();
            }
        }

        private SubjectData? _subjectdata = null;
        public SubjectData? SubjectData
        {
            get => _subjectdata; 
            set
            {
                _subjectdata = value;
                OnPropertyChanged();
            }
        }

        private string _content = string.Empty;
        public string Content
        {
            get => _content; 
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        private bool _isinform = false;
        public bool IsInform
        {
            get => _isinform; 
            set
            {
                _isinform = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _date = new DateTime(1111,1,1);
        public DateTime? Date
        {
            get => _date; 
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _color = new SolidColorBrush(Colors.White);
        public SolidColorBrush Color
        {
            get => _color; 
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        private Priority? _priority = null;
        public Priority? Priority
        {
            get => _priority; 
            set
            {
                _priority = value;
                switch (value)
                {
                    case Scripts.Priority.高:
                        PrioritySelectedIndex = 0;
                        break;
                    case Scripts.Priority.中:
                        PrioritySelectedIndex = 1;
                        break;
                    case Scripts.Priority.低:
                        PrioritySelectedIndex = 2;
                        break;
                }
                OnPropertyChanged();
            }
        }
        private string _remark = string.Empty;
        public string Remark
        {
            get => _remark; 
            set
            {
                _remark = value;
                OnPropertyChanged();
            }
        }


        public ICommand SaveDataCommand { get;private set; }

        private void initList()
        {
            PriorityLists = new ObservableCollection<PriorityList>();
            PriorityLists = [
                new PriorityList("高", Scripts.Priority.高),
                new PriorityList("中",Scripts.Priority.中),
                new PriorityList("低",Scripts.Priority.低),
            ];
        }
    }

    public class PriorityList
    {
        public PriorityList(string name,Priority priorityL)
        {
            Name = name;
            Priority = priorityL;
        }
        public string Name { get; set; }
        public Priority Priority { get; set; }
    }
}
