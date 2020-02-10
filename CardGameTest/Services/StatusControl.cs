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
        public int CurrentPoison { get; private set; }
        public int CurrentBurn { get; private set; }
        public int CurrentLock { get; private set; }
        public Action StatusList { get; private set; }

        public StatusControl(Entity entity)
        {
            _entity = entity;
        }

        public void HasAny()
        {
            //if (_entity.Status.Shield > 0)
            if (_entity.Status.Poison > 0) StatusList += Poison;
            if (_entity.Status.Lock > 0) StatusList += Lock;
            //if (_entity.Status.Blind > 0) ;
            if (_entity.Status.Burn > 0) StatusList += Burn;
            //if (_entity.Status.Curse > 0) ;
            //if (_entity.Status.Frost > 0) ;
            
        }

        private void Poison()
        {
            Game.Damage(_entity, _entity.Status.Poison);
            _entity.Status.Poison--;
        }

        private void Lock()
        {
            for (int i = 0; i < _entity.Status.Lock; i++)
            {
                if (i > _entity.Dice.Count - 1) break;
                _entity.Dice[i].IsLocked = true;
            }

            _entity.Status.Lock = 0;
        }

        private void Burn()
        {
            int aux = 0;
            while (!(aux > _entity.Dice.Count - 1) && _entity.Status.Burn > 0)
            {
                while (!(aux > _entity.Dice.Count - 1) && _entity.Dice[aux].IsLocked)
                {
                    aux++;                                     
                }                
                _entity.Dice[aux].IsBurned = true;
                _entity.Status.Burn--;
                aux++;                
            }

            _entity.Status.Burn = 0;
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
                        statusBar = string.Concat(statusBar, $"   {status.Name}: {(int)status.GetValue(_entity.Status)}");
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
                    statusBar = Regex.Replace(statusBar, $"{status.Name}: {typeof(StatusControl).GetProperty("Current" + status.Name).GetValue(this).ToString()}", "");
                }
            }
            return statusBar;
        }

        public void ActivateStatus()
        {
            Console.WriteLine(_entity.Status.Poison);
            if (StatusList != null)
            {
                StatusList();
                StatusList = null;
            }
        }
    }
}
