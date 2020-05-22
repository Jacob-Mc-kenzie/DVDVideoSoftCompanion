namespace DataManagement
{
    public class MemberCollection
    {
        private Member[] registeredMembers;
        int index;

        public MemberCollection()
        {
            index = 0;
            registeredMembers = new Member[10];
        }
        public void RegisterMember(Member m, out string message)
        {
            for (int i = 0; i < index; i++)
            {
                if (registeredMembers[i].userName == m.userName)
                {
                    message = "Error, name taken";
                    return;
                }
            }
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
        public Member GetMember(string name, out string message)
        {
            if(index == 0)
            {
                message = "No Members have yet been registered";
                return null;
            }
            string[] fullName = name.Split(' ');
            if(fullName.Length == 1)
            {
                for (int i = 0; i < index; i++)
                {
                    if(registeredMembers[i].userName == name)
                    {
                        message = "Success";
                        return registeredMembers[i];
                    }
                }
            }
            else if (fullName.Length > 2)
            {
                message = "Name invalid format";
                return null;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    if (registeredMembers[i].userName == (fullName[1]+fullName[0]))
                    {
                        message = "Success";
                        return registeredMembers[i];
                    }
                }
            }
            message = "Failed to find a match";
            return null;
        }
    }
}
