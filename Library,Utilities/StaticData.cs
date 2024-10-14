using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Utilities
{
    public static class StaticData
    {
        public const string LibrarianRole = "Librarian";
        public const string MemberRole = "Member";
        public const int ReturnDays = 14;
        public const string ConfirmedByUser = "Waiting for a librarian approval";
        public const string InCart = "InCart";
        public const string ApprovedByAdmin = "Borrowing request approved";
        public const string DisaprrovedByAdmin = "Borrowing request disapproved";
        public const string Returned = "Book returned";
        public const decimal PenaltyPerDay = 5;
        public const int pageSize = 8;
    }
}