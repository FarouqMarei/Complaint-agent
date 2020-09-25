using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Utilities
{
    public class Enum
    {
        public enum UserType
        {
            Customer = 1,
            Admin = 2
        }

        public enum StatusType
        {
            Failed = 0,
            Success = 1
        }

        public enum ComplaintStatus
        {
            Pending = 1,
            Resolved = 2,
            Dismissed = 3
        }

        public enum ComplaintType
        {
            Complaint = 1,
            GeneralQuery = 2
        }
    }
}
