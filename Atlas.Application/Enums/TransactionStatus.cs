using System;
namespace Atlas.Application.Enums
{
    public enum TransactionStatus
    {
        STATE_NEW           =  0,
        STATE_IN_PROGRESS   =  1,
        STATE_DONE          =  2,
        STATE_CANCELED      = -1,
        STATE_POST_CANCELED = -2
    }
}

