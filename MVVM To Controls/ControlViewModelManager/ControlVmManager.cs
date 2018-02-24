using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_To_Controls.ControlViewModelManager
{
    public class ControlVmManager
    {
        internal ControlVmManager() { }
        private static ControlVmManager _Default = new ControlVmManager();

        public static ControlVmManager Default
        {
            get { return _Default; }
        }
        public static void ReSetInstance()
        {
            _Default = new ControlVmManager();
        }
        private Dictionary<object, object> ViewModelInsts 
            = new Dictionary<object, object>();

        public void RegisterInstance<TVIEWMODEL>(object key)
            where TVIEWMODEL: ControlViewModelBase, new()
        {
            ViewModelInsts.Add(key, new TVIEWMODEL());
        }
        public void UnRegisterInstance<TVIEWMODEL>(object key)
            where TVIEWMODEL : ControlViewModelBase, new()
        {
            ViewModelInsts.Remove(key);
        }
        public TVIEWMODEL GetInstance<TVIEWMODEL>(object key)
            where TVIEWMODEL : ControlViewModelBase, new()
        {
            return (TVIEWMODEL)ViewModelInsts[key];
        }
        public void CleanAllInstance()
        {
            ViewModelInsts.Clear();
        }        
        class VmInst<TVIEWMODEL>
            where TVIEWMODEL: ControlViewModelBase
        {
            public TVIEWMODEL ViewModel { get; set; }
            internal VmInst() { }
        }
    }
}
