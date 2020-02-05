using CardGameTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Services
{
    class StatusControl
    {
        private Entity _entity;
        public int CurrentShield { get; private set; }
        public Action ActivateStatus { get; private set; }

        public StatusControl(Entity entity)
        {
            _entity = entity;
        }

        public bool HasAny()
        {
            bool check = false;

            if (_entity.Status.Shield > 0) check = true;
            if (_entity.Status.Poison > 0)
            {
                ActivateStatus += Poison;
                check = true;
            }
            if (_entity.Status.Blind > 0) check = true;
            if (_entity.Status.Burn > 0) check = true;
            if (_entity.Status.Curse > 0) check = true;
            if (_entity.Status.Frost > 0) check = true;
            if (_entity.Status.Lock > 0) check = true;

            return check;
        }

        private void Poison()
        {
            Game.Damage(_entity, _entity.Status.Poison);
            _entity.Status.Poison--;
        }

        public string GetStatusInfo(string statusBar)
        {
            //CLEAN THIS SHIT!!!
            Type statusSheet = _entity.Status.GetType();
            PropertyInfo[] statusList = statusSheet.GetProperties();
            foreach (PropertyInfo status in statusList)
            {
                if ((int)status.GetValue(_entity.Status) > 0 && (int)status.GetValue(_entity.Status) != (int)typeof(StatusControl).GetProperty("Current" + status.Name).GetValue(this))
                {
                    if (!statusBar.Contains(status.Name))
                    {                        
                        statusBar = string.Concat(statusBar, $"{status.Name}: {(int)status.GetValue(_entity.Status)}   ");
                        typeof(StatusControl).GetProperty("Current" + status.Name).SetValue(this, (int)status.GetValue(_entity.Status));                        
                    }
                    else
                    {                        
                        statusBar = Regex.Replace(statusBar, $"{status.Name}: {typeof(StatusControl).GetProperty("Current" + status.Name).GetValue(this).ToString()}", $"{ status.Name}: {(int)status.GetValue(_entity.Status)}");
                        typeof(StatusControl).GetProperty("Current" + status.Name).SetValue(this, (int)status.GetValue(_entity.Status));                        
                    }
                }
                else if ((int)status.GetValue(_entity.Status) <= 0 && statusBar.Contains(status.Name))
                {
                    //statusBar.Remove(playerStatus.IndexOf("Poison: " + _player.Status.Poison));
                }
            }
            return statusBar;
        }
    }
}
