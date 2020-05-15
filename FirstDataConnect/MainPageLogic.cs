using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLService;
using PCLUtilities;
using System.Management;
using System.Threading;

namespace FirstDataConnect
{
    public class MainPageLogic : ObservableObject
    {
        private ObservableCollection<CompanionInfo> _PairedDevices;
        public ObservableCollection<CompanionInfo> PairedDevices
        {
            get => _PairedDevices;
            set => Set(ref _PairedDevices, value);
        }

        public void RefreshDevices()
        {
            RefreshDevicesList();
        }

        private async void RefreshDevicesList()
        {
            ObservableCollection<CompanionInfo> listPerUSB = null;
            ObservableCollection<CompanionInfo> temporalList = new ObservableCollection<CompanionInfo>();
            
            try
            {
                listPerUSB = new ObservableCollection<CompanionInfo>(await PclUtilities.GetUSBDevices());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetUSBDevices : 0x" + ex.HResult.ToString("X") + ", " + ex.Message);
            }

            if (listPerUSB != null)
            {
                foreach (var peer in listPerUSB)
                    temporalList.Add(peer);
            }

            PairedDevices = temporalList;
        }
    }
}
