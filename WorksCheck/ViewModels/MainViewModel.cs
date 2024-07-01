using GongSolutions.Wpf.DragDrop;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorksCheck.Scripts;
using System.Windows.Media;
using WorksCheck.Views;
using WorksCheck.Resources.UserControls;
using System.Windows.Input;

namespace WorksCheck.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            InitList();
            InitDayRack();

            OneweekAhead = CreateCommand(v =>
            {
                if(difference < 10)
                {
                    difference++;
                    DateTime keydate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(difference*7);
                    //DateTime keydate = DateLists[0].AddDays(difference*7);
                    DateLists.Clear();
                    for (int i = 0; i < 7; ++i)
                    {
                        DateTime calcDate = keydate.AddDays(i);
                        DateLists.Add(calcDate);
                    }
                }
            },null);

            OneweekAgo = CreateCommand(v =>
            {
                if(difference > -10)
                {
                    difference--;
                    DateTime keydate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(difference * 7);
                    DateLists.Clear();
                    for (int i = 0; i < 7; ++i)
                    {
                        DateTime calcDate = keydate.AddDays(i);
                        DateLists.Add(calcDate);
                    }
                }
            },null);
        }

        private int difference = 0;

        private ObservableCollection<SubjectList> _subjectlists;
        public ObservableCollection<SubjectList> Subjectlists
        {
            get => _subjectlists;
            set
            {
                _subjectlists = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateTime> _datelists;
        public ObservableCollection<DateTime> DateLists
        {
            get => _datelists;
            set
            {
                _datelists = value;
                OnPropertyChanged();
            }
        }

        public DayRackCustomDropHandler DayRackCustomDropHandler { get; set; } = new DayRackCustomDropHandler();

        private void InitList()
        {
            Subjectlists = new ObservableCollection<SubjectList>();

            var Japanese = new HomeworkData();
            Japanese.Subject = SubjectData.Japanese;
            var Math = new HomeworkData();
            Math.Subject = SubjectData.Math;
            var English = new HomeworkData();
            English.Subject = SubjectData.English;
            var Social = new HomeworkData();
            Social.Subject = SubjectData.Social;
            var Science = new HomeworkData();
            Science.Subject = SubjectData.Science;
            var HomeEco = new HomeworkData();
            HomeEco.Subject = SubjectData.HomeEco;
            var Info = new HomeworkData();
            Info.Subject = SubjectData.Info;
            var PE = new HomeworkData();
            PE.Subject = SubjectData.PE;
            var Music = new HomeworkData();
            Music.Subject = SubjectData.Music;
            var Art = new HomeworkData();
            Art.Subject = SubjectData.Art;
            var CallG = new HomeworkData();
            CallG.Subject = SubjectData.CallG;

            Subjectlists = [
                     new SubjectList("国語",Japanese),
                     new SubjectList("数学",Math),
                     new SubjectList("英語",English),
                     new SubjectList("社会",Social),
                     new SubjectList("理科",Science),
                     new SubjectList("家庭",HomeEco),
                     new SubjectList("情報",Info),
                     new SubjectList("保体",PE),
                     new SubjectList("音楽",Music),
                     new SubjectList("美術",Art),
                     new SubjectList("書写",CallG),
                ];
        }

        private void InitDayRack()
        {
            DateLists = new ObservableCollection<DateTime>();
            DateTime targetDate = DateTime.Today;

            DateTime keyDate = targetDate.AddDays(-(int)targetDate.DayOfWeek);
            for(int i = 0; i < 7; ++i)
            {
                DateTime calcDate = keyDate.AddDays(i);
                DateLists.Add(calcDate);
            }
        }

        public ICommand OneweekAhead { get;private set; }

        public ICommand OneweekAgo { get; private set; }
    }
    
    public class SubjectList
    {
        public SubjectList(string subjectname,HomeworkData homeworkdata)
        {
            SubjectName = subjectname;
            HomeworkData = homeworkdata;
        }

        public string SubjectName { get; set; }
        public HomeworkData HomeworkData { get; set; }
    }

    public class DayRackCustomDropHandler : IDropTarget
    {
        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
            dropInfo.Effects = DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data.GetType().Equals(typeof(SubjectList)))
            {
                //Dayrackに追加する処理
                var SHViewModel = new SetHomeworkViewModel();
                SHViewModel.SubjectName = ((SubjectList)dropInfo.Data).SubjectName;

                SHViewModel.Content = ((SubjectList)dropInfo.Data).HomeworkData.Content;
                SHViewModel.IsInform = ((SubjectList)dropInfo.Data).HomeworkData.IsInform;
                SHViewModel.Date = ((DayRack)dropInfo.VisualTarget).Date;
                SHViewModel.SubjectData = ((SubjectList)dropInfo.Data).HomeworkData.Subject;
                SHViewModel.Color = ((SubjectList)dropInfo.Data).HomeworkData.Color;
                //SHViewModel.Priority = ((SubjectList)dropInfo.Data).HomeworkData.Priority;　priorityは確定でnullのため
                SHViewModel.Remark = ((SubjectList)dropInfo.Data).HomeworkData.Remark;

                var SHWindow = new SetHomeworkWindow(SHViewModel);
                SHViewModel.View = SHWindow;
                SHViewModel.DayRack = (DayRack)dropInfo.VisualTarget;
                SHWindow.ShowDialog();
            }
        }
    }

    public class DropTargetHighlightAdorner : DropTargetAdorner
    {
        private readonly Brush _brush;
        private readonly Pen _pen;
        public DropTargetHighlightAdorner(UIElement adornedElement,DropInfo dropInfo) : base(adornedElement, dropInfo)
        {
            _brush = Brushes.Transparent;
            _pen = new Pen(FindResource("AppmainColor") as SolidColorBrush,0.7);
            _pen.Freeze();
            this.SetValue(SnapsToDevicePixelsProperty, true);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var visualTarget = this.DropInfo.VisualTarget;
            if(visualTarget != null)
            {
                var translatedPoint = visualTarget.TranslatePoint(new Point(), this.AdornedElement);
                translatedPoint.Offset(1, 1);
                var bounds = new Rect(translatedPoint, new Size(visualTarget.RenderSize.Width, visualTarget.RenderSize.Height));

                drawingContext.DrawRectangle(_brush, _pen, bounds);
            }
        }
    }
}
