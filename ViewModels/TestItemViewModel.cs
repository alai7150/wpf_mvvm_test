using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfAppRohdeSchwarzTest
{
    public class TestItemViewModel:BaseViewModel
    {
        private Action UpdateStatusAction = null;

        #region Properties
        /// <summary>
        /// Major revision
        /// </summary>
        public int Major { get; set; }

        /// <summary>
        /// Minor revision
        /// </summary>
        public int Minor { get; set; }

        /// <summary>
        /// Get test name for text binding
        /// </summary>
        public string Name { get { return Minor != 0 ? $"Test {this.Major}.{this.Minor}" : $"Test {this.Major}"; } }


        /// <summary>
        /// Indicate the check status of the item
        /// </summary>
        private bool? _IsChecked = false;
        public bool? IsChecked 
        {
            get
            {
                return this._IsChecked;
            }
            set
            {
                //update self check status
                this._IsChecked = (Children == null || Children.Count == 0) & !value.HasValue? false : value;

                //update children's check status
                if (_IsChecked.HasValue)
                {
                    foreach (var tivm in Children)
                    {
                        tivm.IsChecked = value;
                    }
                }

                //Update parent check status
                if (ParentItem != null)
                    ParentItem.UpdateCheckStatus();

                //Trigger out status update
                if (UpdateStatusAction != null)
                    UpdateStatusAction();
            }
        }

        /// <summary>
        /// Indicate whether there is any chilren, Only Major item can be expanded
        /// </summary>
        public bool CanExpand { get { return Children.Count > 0 ? true : false; } }

        public bool _IsExpanded = true;
        public bool IsExpanded {
            get
            {
                return _IsExpanded;
            }
            set
            {
                _IsExpanded = value;
            }
        }
        #endregion


        #region Variables
        /// <summary>
        /// Children TestItem list
        /// </summary>
        public ObservableCollection<TestItemViewModel> Children { get; set; } = new ObservableCollection<TestItemViewModel>();
        private TestItemViewModel ParentItem = null;
        #endregion


        #region Methods
        public void UpdateCheckStatus()
        {
            if (Children.Count > 0)
            {
                int checkedChildrenCount = Children.Where(t => t.IsChecked == true).Count();
                if (checkedChildrenCount == 0)
                {
                    this.IsChecked = false;
                    return;
                }
                if(checkedChildrenCount == Children.Count)
                {
                    this.IsChecked = true;
                    return;
                }
                this.IsChecked = null;
            }
        }
        #endregion


        #region Constructor
        public TestItemViewModel(int major, int minor, Action updateStatusAction=null, TestItemViewModel parent = null)
        {
            this.Major = major;
            this.Minor = minor;
            this.IsChecked = false;
            this.ParentItem = parent;
            UpdateStatusAction = updateStatusAction;
            //this.IsExpanded = false;
            if (minor == 0)
                this.Children = new ObservableCollection<TestItemViewModel>(DataMock.GetMinorItems(this.Major, updateStatusAction, this));
            else
                this.Children = new ObservableCollection<TestItemViewModel>();
        }
        #endregion



    }
}
