using System;
using System.Collections.Generic;

class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public User(string name, string email, string role)
    {
        Name = name;
        Email = email;
        Role = role;
    }

    public override string ToString()
    {
        return $"User(Name: {Name}, Email: {Email}, Role: {Role})";
    }
}

class UserManager
{
    private List<User> users = new List<User>();

    public void AddUser(string name, string email, string role)
    {
        if (!UserExists(email))
        {
            User user = new User(name, email, role);
            users.Add(user);
            Console.WriteLine($"Пользователь {name} добавлен.");
        }
        else
        {
            Console.WriteLine($"Пользователь с email {email} уже существует.");
        }
    }

    public void RemoveUser(string email)
    {
        User user = FindUserByEmail(email);
        if (user != null)
        {
            users.Remove(user);
            Console.WriteLine($"Пользователь с email {email} удален.");
        }
        else
        {
            Console.WriteLine($"Пользователь с email {email} не найден.");
        }
    }

    public void UpdateUser(string email, string newName = null, string newRole = null)
    {
        User user = FindUserByEmail(email);
        if (user != null)
        {
            if (newName != null)
                user.Name = newName;
            if (newRole != null)
                user.Role = newRole;

            Console.WriteLine($"Информация о пользователе с email {email} обновлена.");
        }
        else
        {
            Console.WriteLine($"Пользователь с email {email} не найден.");
        }
    }

    private User FindUserByEmail(string email)
    {
        foreach (User user in users)
        {
            if (user.Email == email)
                return user;
        }
        return null;
    }

    private bool UserExists(string email)
    {
        return FindUserByEmail(email) != null;
    }

    public void ListUsers()
    {
        if (users.Count > 0)
        {
            foreach (User user in users)
            {
                Console.WriteLine(user);
            }
        }
        else
        {
            Console.WriteLine("Пользователей нет.");
        }
    }
}

class Program
{
    static void Main()
    {
        UserManager manager = new UserManager();

        manager.AddUser("1234", "1234@gmail.com", "Admin");
        manager.AddUser("1234", "1234@gmail.com", "User");

        manager.ListUsers();

        manager.UpdateUser("1234@gmail.com", newName: "Jane Doe", newRole: "Admin");

        manager.RemoveUser("1234@gmail.com");

        manager.ListUsers();
    }
}
