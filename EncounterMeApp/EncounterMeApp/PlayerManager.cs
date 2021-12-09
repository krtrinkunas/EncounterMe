using EncounterMeApp.Models;
using EncounterMeApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EncounterMeApp
{
    public class PlayerManager
    {
        private IPlayerService playerService;

        public PlayerManager(IPlayerService _playerService)
        {
            playerService = _playerService;
        }
        public async Task<Player> validateUser(Player newPlayer)
        {
            if (string.IsNullOrEmpty(newPlayer.NickName))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Email))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Firstname))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Lastname))
            {
                return null;
            }
            if (string.IsNullOrEmpty(newPlayer.Password))
            {
                return null;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(newPlayer.Email);
            if (!match.Success)
            {
                return null;
            }
            await playerService.AddPlayer(newPlayer);
            return newPlayer;
        }
    }
}
