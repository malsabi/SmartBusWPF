namespace SmartBusWPF.Models.Student
{
    public class StudentModel
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int GradeLevel { get; set; }
        public string Address { get; set; }
        public int BelongsToBusID { get; set; }
        public bool IsAtSchool { get; set; } = false;
        public bool IsAtHome { get; set; } = true;
        public bool IsOnBus { get; set; } = false;
        public int ParentID { get; set; }

        public string UserImage { get; } = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABmJLR0QA/wD/AP+gvaeTAAAB/ElEQVRYCe2WSytEYRjHZ1yisMCwsLEWVvaTDZtJySWXYmdh7bKykK2FFEnNRzCi5nsoJSWXLFAs3HMp5/j98850jM6cdziTzZz+v+d5zvtc3tc7GiKR0lO6gX++gWih+7uu20zPNCSgFaRzTBo2otHoDb44YvMxeAY/PZIYKcruDB4CB6QUJg71BsVa49V1MAOhHoKBTXAP0oLfcJJzIN1iGv3qCl5n2BJIqaBmitIgLUbCepi2D1I8aCZF3SDtBdVa55n2BFJNUBNFdSA9BNUqXybzn9ge4NQcssv4fC5Tc5KvKJOzPcCOaZg1Pp+bM8ld4//u+EBjcAeS7yFIzoOk2tjfd/ZMYOoQ6EsG525h4lBtUJwilhzMoKc1vJDBk/AKfnohMRHejp5JDNYNXOGDdElBeDfAsApYBa+OeemAmKETfwperfBS7vkZfhcyZA2kN8wMZG5hk7gFGmAdpAvMLLyDtPy7XU0XE8ZBesP0aBnfB3rHfZN+NxKmppeMahx8n9YKhsYqOHO/ninvAJZ0/dv4a0MK355TM82adISp9OasYppGQdrH2H5hZWfTUw4HIA1nEzlBvsH9pjbJv1mOia0dPR8UJ0HKzFJsB8fW1eHcNruOn1U066PCuYc/swErdD2CVBtQ6pumuQ4kqz/NvoNKidINFPMGPgEDVIUUxj5oUwAAAABJRU5ErkJggg==";
    }
}