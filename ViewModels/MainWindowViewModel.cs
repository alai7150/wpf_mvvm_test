using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace WpfAppRohdeSchwarzTest
{
    public class MainWindowViewModel:BaseViewModel
    {
        /// <summary>
        /// The Test Item list
        /// </summary>
        public ObservableCollection<TestItemViewModel> Items { get; set; }

        public bool HasItemChecked { get; set; }
        public void UpdateItemCheckedStatus()
        {
            HasItemChecked = Items.Where(t => t.IsChecked != false).Count() > 0 ? true : false;
        }

        //Command to expand all treeview items
        public ICommand ExpandAllCommand { get; set; }
        private void ExpandAllFunc()
        {
            foreach(TestItemViewModel itm in Items)
            {
                itm.IsExpanded = true;
            }
        }

        //command to collapse all treview items
        public ICommand CollapseAllCommand { get; set; }
        private void CollapseAllFunc()
        {
            foreach (TestItemViewModel itm in Items)
            {
                itm.IsExpanded = false;
            }
        }

        //command to reset all treeview item check status
        public ICommand ResetAllCommand { get; set; }
        public void ResetAllItemCheckStatusFunc()
        {
            foreach (TestItemViewModel itm in Items)
            {
                itm.IsChecked = false;
            }
        }

        //command to execute tests (display checked tests' name)
        public ICommand ExecuteAllCommand { get; set; }
        private string GetTestNames(TestItemViewModel tivm)
        {
            string tnames = "";
            if(tivm.IsChecked != false)
            {
                if(tivm.Minor != 0)
                    tnames = tivm.Name +"\r\n";
                if(tivm.Children.Count>0)
                {
                    foreach(TestItemViewModel ti in tivm.Children)
                    {
                        tnames += GetTestNames(ti);
                    }
                }
            }
            return tnames;
        }
        public void ExecuteTestsFunc()
        {
            //All all checked test names
            string msg = "";
            foreach(TestItemViewModel itm in Items)
            {
                string itemNames = GetTestNames(itm);
                if (!string.IsNullOrEmpty(itemNames))
                {
                    msg += itemNames;
                }
            }

            //display msg
            if(!string.IsNullOrEmpty(msg.Trim()))
                MessageBox.Show(msg);
            else
                MessageBox.Show("No test is checked.");
        }

        //Constructor
        public MainWindowViewModel()
        {
            this.ExpandAllCommand = new RelayCommand(ExpandAllFunc);
            this.CollapseAllCommand = new RelayCommand(CollapseAllFunc);
            this.ExecuteAllCommand = new RelayCommand(ExecuteTestsFunc);
            this.ResetAllCommand = new RelayCommand(ResetAllItemCheckStatusFunc);
            this.Items = new ObservableCollection<TestItemViewModel>(DataMock.GetMajorItems(UpdateItemCheckedStatus));
        }
    }
}
