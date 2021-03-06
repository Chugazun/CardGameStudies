﻿using CardGameTest.Entities;
using CardGameTest.Entities.Cards;
using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGameTest.Services
{
    class StatusControl
    {
        private Entity _entity;
        private List<Card> changedCards = new List<Card>();
        private string persistentStatus = "Shock Burn Frost Shield Poison Fury Thorns";
        public int CurrentShield { get; private set; }
        public int CurrentPoison { get; private set; }
        public int CurrentBurn { get; private set; }
        public int CurrentFrost { get; private set; }
        public int CurrentLock { get; private set; }
        public int CurrentBlind { get; private set; }
        public int CurrentShock { get; private set; }
        public int CurrentCurse { get; private set; }
        public int CurrentResistance { get; private set; }
        public int CurrentFury { get; private set; }
        public int CurrentReEquip { get; private set; }
        public int CurrentDodge { get; private set; }
        public int CurrentWeaken { get; private set; }
        public int CurrentThorns { get; private set; }
        public Action StatusList { get; private set; }

        public StatusControl(Entity entity)
        {
            _entity = entity;
        }

        public void HasTurnStart()
        {
            if (_entity.Status.Thorns > 0) _entity.Status.Thorns = 0;

            if (_entity.Status.Shock > 0) StatusList += Shock;
            if (_entity.Status.Weaken > 0) StatusList += Weaken;

            if (_entity.Status.Poison > 0) StatusList += Poison;
            if (_entity.Status.Lock > 0) StatusList += Lock;
            if (_entity.Status.Blind > 0) StatusList += Blind;
            if (_entity.Status.Burn > 0) StatusList += Burn;
            if (_entity.Status.Frost > 0) StatusList += Frost;
        }

        private void Shock()
        {
            for (int i = 0; i < _entity.Status.Shock; i++)
            {
                if (i > _entity.GetCards().Count - 1) break;
                int handPos = new Random().Next(_entity.GetCards().Count);
                Card tempCard = _entity.GetCardAt(handPos);
                if (!(tempCard is Shocked))
                {
                    Shocked shockedCard = new Shocked(tempCard.ID)
                    {
                        Name = tempCard.Name + " [S]"
                    };
                    changedCards.Add(tempCard);
                    _entity.GetCards()[handPos] = shockedCard;
                }
            }

            _entity.Status.Shock = 0;
        }

        private void Weaken()
        {            
            for (int i = 0; i < _entity.Status.Weaken; i++)
            {
                if (i > _entity.GetCards().Count - 1) break;
                int handPos = new Random().Next(_entity.GetCards().Count);               
                Card tempCard = _entity.GetCardAt(handPos);
                if (!tempCard.IsWeakened)
                {
                    Game.Log.AppendLine($"{(tempCard.Name.Contains("(") ? tempCard.Name.Substring(0, tempCard.Name.IndexOf("(") - 1) : tempCard.Name)} was Weakened!".Pastel(Color.DarkOrange));
                    tempCard.Weaken();
                    //Game.Log.AppendLine($"{tempCard.Name.Substring(0, tempCard.Name.IndexOf("-"))} was Weakened!".Pastel(Color.DarkOrange));
                }
            }
            
            _entity.Status.Weaken = 0;
        }

        private void Poison()
        {
            Game.TrueDamage(_entity, _entity.Status.Poison);
            Game.Log.AppendLine($"{_entity.GetType().Name} took {_entity.Status.Poison} Poison damage!".Pastel(Color.Purple));
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

        private void Blind()
        {
            int aux = 0;
            while (!(aux > _entity.Dice.Count - 1) && _entity.Status.Blind > 0)
            {
                while (!(aux > _entity.Dice.Count - 1) && _entity.Dice[aux].IsLocked)
                {
                    aux++;
                }
                _entity.Dice[aux].IsBlinded = true;
                _entity.Status.Blind--;
                aux++;
            }

            _entity.Status.Blind = 0;
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

        private void Frost()
        {
            for (int i = 0; i < _entity.Status.Frost; i++)
            {
                if (i > _entity.Dice.Count - 1) break;
                Die die = _entity.Dice.FirstOrDefault(d => d.Value == _entity.Dice.Max(d => d.Value));
                Game.Log.Append($"#D{_entity.Dice.FindIndex(d => d == die) + 1} - ".Pastel(Color.Cyan));
                Game.Log.Append(die.ToString().Pastel(Color.Cyan));
                Game.Log.AppendLine(" was Frozen!".Pastel(Color.Cyan));
                die.Value = 1;
            }

            _entity.Status.Frost = 0;
        }

        private void Fury(Card card, int diceVal)
        {
            card.act(diceVal);
            card.Used = false;
            Game.Log.AppendLine($"{(card.Name.Contains("(") ? card.Name.Substring(0, card.Name.IndexOf("(") - 1) : card.Name)} Reused".Pastel(Color.Red));
            _entity.Status.Fury--;
        }

        private void ReEquip(Card card, int diceVal)
        {
            card.act(diceVal);
            card.ResetCard();
            Game.Log.AppendLine($"{(card.Name.Contains("(") ? card.Name.Substring(0, card.Name.IndexOf("(") - 1) : card.Name)} Re-Equipped!".Pastel(Color.LightGreen));
            _entity.Status.ReEquip--;
        }

        public bool Curse(byte selectedCard)
        {
            int curseChance = new Random().Next(100);

            if (curseChance < 50)
            {
                _entity.Status.Curse--;
                _entity.GetCards().FirstOrDefault(c => c.ID == selectedCard).Used = true;
                Game.Log.AppendLine("Curse Activated!".Pastel(Color.DarkGray));
                return true;
            }

            return false;
        }

        public void Dodge()
        {
            Game.Log.AppendLine("Attack Dodged!".Pastel(Color.Yellow));
            _entity.Status.Dodge--;
        }

        public void ActivateThorns(Entity target)
        {
            Game.Damage(target, _entity.Status.Thorns);
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
            StatusList?.Invoke();
            StatusList = null;
        }

        public bool OnCardPlayStatus(Card card, int diceVal)
        {
            if (_entity.Status.Curse > 0 && Curse(card.ID))
            {
                return false;
            }

            if (_entity.Status.Fury > 0)
            {
                Fury(card, diceVal);
            }

            if (_entity.Status.ReEquip > 0)
            {
                ReEquip(card, diceVal);
                return false;
            }

            return true;
        }

        public void RemoveShock(int id)
        {
            int index = _entity.GetCards().FindIndex(c => c.ID == id);
            _entity.GetCards()[index] = changedCards.FirstOrDefault(c => c.ID == id);
            changedCards.Remove(changedCards.FirstOrDefault(c => c.ID == id));
        }

        public void ResetChangedCards()
        {
            List<Card> _changedCards = _entity.GetCards().Where(c => c is Shocked).ToList();
            _changedCards.ForEach(c =>
            {
                RemoveShock(c.ID);
            });

            _changedCards = _entity.GetCards().Where(c => c.IsWeakened).ToList();
            _changedCards.ForEach(c =>
            {
                c.Normalize();
            });
        }

        public void ResetEndTurnStatus()
        {
            Type status = _entity.Status.GetType();

            List<PropertyInfo> properties = status.GetProperties().Where(p => (int)p.GetValue(_entity.Status) > 0).ToList();

            properties.ForEach(v =>
            {
                if (!persistentStatus.Contains(v.Name)) v.SetValue(_entity.Status, 0);
            });
        }
    }
}
