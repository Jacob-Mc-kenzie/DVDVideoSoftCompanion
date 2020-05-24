namespace DataManagement
{
    /// <summary>
    /// A simple collection of members stored in an array.
    /// </summary>
    public class MemberCollection
    {
        public Member[] registeredMembers;
        /// <summary>
        /// Keep track of how many members have been added to the array.
        /// </summary>
        int index;
        /// <summary>
        /// Initilises a member collection of lenght 10
        /// </summary>
        public MemberCollection()
        {
            index = 0;
            registeredMembers = new Member[10];
        }
        /// <summary>
        /// Adds a member to the colelction if there are no conflicts.
        /// </summary>
        /// <param name="m">The member to add</param>
        /// <param name="message">The result of the attempt</param>
        public void RegisterMember(Member m, out string message)
        {
            //first check if the name is taken===================
            for (int i = 0; i < index; i++)
            {
                if (registeredMembers[i].userName == m.userName)
                {
                    message = "Error, name taken";
                    return;
                }
            }
            //next check if there is room in the array==========
            if (index < 9)
            {
                message = "Success";
                registeredMembers[index] = m;
                index++;
            }
            else
            {
                message = "Error, Too many users";
            }
                
        }
        /// <summary>
        /// Gets a member if they exist.
        /// </summary>
        /// <param name="name">either the username, or full name of a user</param>
        /// <param name="message">The result of the query.</param>
        /// <returns>The member if found</returns>
        public Member GetMember(string name, out string message)
        {
            //first check if there are any members============== 
            if(index == 0)
            {
                message = "No Members have yet been registered";
                return null;
            }
            string[] fullName = name.Split(' ');
            //If there is only one item in the array, then it must be a username
            if(fullName.Length == 1)
            {
                //Look for the user============================
                for (int i = 0; i < index; i++)
                {
                    if(registeredMembers[i].userName == name)
                    {
                        message = "Success";
                        return registeredMembers[i];
                    }
                }
            }
            //Otherwise if there are more than 2 words, we don't know what it is, so return.
            else if (fullName.Length > 2)
            {
                message = "Name invalid format";
                return null;
            }
            //If it's 2 long it must be a full name=============
            else
            {
                //Look for the member buy concatonating to make a username
                for (int i = 0; i < index; i++)
                {
                    if (registeredMembers[i].userName == (fullName[1]+fullName[0]))
                    {
                        message = "Success";
                        return registeredMembers[i];
                    }
                }
            }
            //If we havent returned by now, then there was no match
            message = "Failed to find a match";
            return null;
        }
        /// <summary>
        /// Gets a member if they exist.
        /// </summary>
        /// <param name="name">either the username, or full name of a user</param>
        /// <param name="message">The result of the query</param>
        /// <param name="ind">The index of the member if found</param>
        /// <returns>The member if found</returns>
        public Member GetMember(string name, out string message, out int ind)
        {
            //Operates the same as the above function, just also returning the index, or -1;
            if (index == 0)
            {
                message = "No Members have yet been registered";
                ind = -1;
                return null;
            }
            string[] fullName = name.Split(' ');
            if (fullName.Length == 1)
            {
                for (int i = 0; i < index; i++)
                {
                    if (registeredMembers[i].userName == name)
                    {
                        message = "Success";
                        ind = i;
                        return registeredMembers[i];
                    }
                }
            }
            else if (fullName.Length > 2)
            {
                message = "Name invalid format";
                ind = -1;
                return null;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    if (registeredMembers[i].userName == (fullName[1] + fullName[0]))
                    {
                        message = "Success";
                        ind = i;
                        return registeredMembers[i];
                    }
                }
            }
            message = "Failed to find a match";
            ind = -1;
            return null;
        }
    }
}
