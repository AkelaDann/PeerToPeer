using FileShare.Contract.FileShare;
using FileShare.Logics.FileShareManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Web.FileShareServices.FileShareHostServices
{
    public class FileShareHostService : IFileShareHostService
    {
        private ServiceHost _host;
        public FileShareHostService(int port,string uri)
        {
            Port = port;
            Uri = uri;
            IsStarted = false;
        }

        public int Port { get; }
        public String Uri { get; }
        public bool IsStarted { get; set; }
        

        public bool Start()
        {
            var uri = new Uri[1];
            if(!string.IsNullOrEmpty(Uri)&& Port > 0)
            {
                var address = $"net.tcp://{Uri}:{Port}/FileShare";
                uri[0] = new Uri(address);
                IFileShareService fileShare = new FileManager();
                _host = new ServiceHost(fileShare, uri);
                var binding = new NetTcpBinding(SecurityMode.None);
                _host.AddServiceEndpoint(typeof(IFileShareService), binding, "");
                _host.Opened += HostOnOpened;
                _host.Open();
                return IsStarted;
            }
            return IsStarted;
        }

        private void HostOnOpened(object sender, EventArgs e)
        {
            IsStarted = true;
        }

        public bool Stop()
        {
            if(_host != null)
            {
                _host.Closed += HostOnClosed;
                _host.Close();
                _host = null;
                return IsStarted;
            }

            return IsStarted;
        }

        private void HostOnClosed(object sender, EventArgs e)
        {
            IsStarted = true;

        }
    }
}
