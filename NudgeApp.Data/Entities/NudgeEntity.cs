using System;
using System.Collections.Generic;
using System.Text;
using NudgeApp.Common.Enums;

namespace NudgeApp.Data.Entities
{
    public class NudgeEntity : DbEntity
    {
        public virtual UserEntity User { get; set; }
        public Guid UserId{ get; set; }
        public virtual EnvironmentalInfoEntity EnvironmentalInfo { get; set; }
        public Guid EnvironmentalInfoId { get; set; }
        public NudgeResult NudgeResult { get; set; }
        public TransportationType TransportationType { get; set; }
    }
}
