using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;
namespace BL
{
    internal partial class BL : BLApi.IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveUser(string name)
        {
            try
            {
                lock (dal)
                {

                    dal.DeleteUser(name);
                }
            }
            catch (Exception e)
            {
                throw new DeletingException(e.Message, e);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User> GetAllUsersBy(Predicate<User> condition)
        {
            var list = from item in GetUsers()
                       let temp= (BO.User)item.CopyPropertiesToNew(typeof(BO.User))
                       where condition(temp)
                       select item;
            return list;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<User> GetUsers()
        {
            lock (dal)
            {

                var list = from item in dal.GetUsers() select (BO.User)item.CopyPropertiesToNew(typeof(BO.User));
                return list;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public User GetUser(string name)
        {
            try
            {
                lock (dal)
                {

                    DO.User user = dal.GetUser(name);
                    return (BO.User)user.CopyPropertiesToNew(typeof(BO.User));
                }
            }
            catch (Exception e)
            { throw new GettingProblemException($"the user with the name {name} is not exist", e); }


        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatingDetailsOfUser(string name, string password)
        {
            //validation
            if (name == "")
                throw new AddingProblemException("invalid Name of user: you didn't enter a name");
            if(password==" ")
                throw new AddingProblemException("invalid password of user: you didn't enter a name");
            if(password.Length<6)
                throw new AddingProblemException("invalid password of user: password should include at least 6 characters");
            //add
            try
            {
                lock (dal)
                {

                    DO.User user = dal.GetUser(name);
                    user.UserPassword = password;
                    dal.UpdateUser(user);
                }
            }
            catch (Exception ex)
            { throw new UpdatingException($"Can't update user with the name {name}", ex); }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddUser(User userToAdd)
        {
            if (userToAdd.UserName == "")
                throw new AddingProblemException("invalid Name of user: you didn't enter a name");
            if (userToAdd.UserPassword == "")
                throw new AddingProblemException("invalid password of user: you didn't enter a name");
            if (userToAdd.UserPassword.Length < 6)
                throw new AddingProblemException("invalid password of user: password should include at least 6 characters");
            try
            {
                lock (dal)
                {

                    DO.User user = (DO.User)userToAdd.CopyPropertiesToNew(typeof(DO.User));
                    dal.AddUser(user);
                }
            }
            catch(Exception e)
            {
                 throw new AddingProblemException($"Can't add user with the name {userToAdd.UserName}", e);

            }


        }

    }
}
