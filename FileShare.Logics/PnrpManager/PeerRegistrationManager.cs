using FileShare.Contract.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Logics.PnrpManager
{
    public class PeerRegistrationManager : IPeerRegistrationRepository
    {
        #region field
        private PeerNameRegistration _peerNameRegistration = null;
        #endregion

        public bool IsPeerRegistered => _peerNameRegistration != null && _peerNameRegistration.IsRegistered();

        public string PeerUri => GetPeerUri();

        private string GetPeerUri()
        {
            return _peerNameRegistration?.PeerName.PeerHostName;
        }

        public PeerName PeerName { get; set; }

        public void StartPeerRegistration(string username, int port)
        {
            PeerName = new PeerName(username, PeerNameType.Unsecured);
            _peerNameRegistration = new PeerNameRegistration(PeerName, port);
            _peerNameRegistration.Start();
        }


        public void StopPeerRegistration()
        {
            _peerNameRegistration?.Stop();
            _peerNameRegistration = null;
        }


    }
}

