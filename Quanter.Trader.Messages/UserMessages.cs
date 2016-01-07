using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Trader.Messages
{
    public enum UserActionType
    {
        LOGIN,
        REGISTER,
        FETCH_MY_STRATEGIES,
        ADD_MY_STRATEGY,
        REMOVE_MY_STRATEGY,
        START_ALL_STRATEGY,
        STOP_ALL_STRATEGY,
    }

    public class UserRequest
    {
        public UserActionType Action { get; set; }

        public Object Body { get; set; }
    }

    public class UserResponse
    {
        public Object Body { get; set; }
    }
}
