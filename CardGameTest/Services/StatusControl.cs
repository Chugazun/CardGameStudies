using CardGameTest.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameTest.Services
{
    class StatusControl
    {
        private readonly Entity _entity;

        public StatusControl(Entity entity)
        {
            _entity = entity;
        }

        public bool HasAny()
        {
            if (_entity.Shield > 0) return true;

            return false;
        }
    }
}
