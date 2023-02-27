using System;

namespace SmartBusWPF.Models.Student
{
    public class StudentModel
    {
        public int ID { get; set; } = 0;
        public string Image { get; set; } = "";
        public string FirstName { get; set; } = "N/A";
        public string LastName { get; set; } = "N/A";
        public string FullName => $"{FirstName} {LastName}";
        public string Gender { get; set; } = "N/A";
        public int GradeLevel { get; set; } = 0;
        public string Address { get; set; } = "N/A";
        public int BelongsToBusID { get; set; } = 0;
        public bool IsAtSchool { get; set; } = false;
        public bool IsAtHome { get; set; } = true;
        public bool IsOnBus { get; set; } = false;
        public int ParentID { get; set; } = 0;
        public DateTime LastScanned { get; set; } = DateTime.MinValue;
        public string UserImage { get; } = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABmJLR0QA/wD/AP+gvaeTAAAB/ElEQVRYCe2WSytEYRjHZ1yisMCwsLEWVvaTDZtJySWXYmdh7bKykK2FFEnNRzCi5nsoJSWXLFAs3HMp5/j98850jM6cdziTzZz+v+d5zvtc3tc7GiKR0lO6gX++gWih+7uu20zPNCSgFaRzTBo2otHoDb44YvMxeAY/PZIYKcruDB4CB6QUJg71BsVa49V1MAOhHoKBTXAP0oLfcJJzIN1iGv3qCl5n2BJIqaBmitIgLUbCepi2D1I8aCZF3SDtBdVa55n2BFJNUBNFdSA9BNUqXybzn9ge4NQcssv4fC5Tc5KvKJOzPcCOaZg1Pp+bM8ld4//u+EBjcAeS7yFIzoOk2tjfd/ZMYOoQ6EsG525h4lBtUJwilhzMoKc1vJDBk/AKfnohMRHejp5JDNYNXOGDdElBeDfAsApYBa+OeemAmKETfwperfBS7vkZfhcyZA2kN8wMZG5hk7gFGmAdpAvMLLyDtPy7XU0XE8ZBesP0aBnfB3rHfZN+NxKmppeMahx8n9YKhsYqOHO/ninvAJZ0/dv4a0MK355TM82adISp9OasYppGQdrH2H5hZWfTUw4HIA1nEzlBvsH9pjbJv1mOia0dPR8UJ0HKzFJsB8fW1eHcNruOn1U066PCuYc/swErdD2CVBtQ6pumuQ4kqz/NvoNKidINFPMGPgEDVIUUxj5oUwAAAABJRU5ErkJggg==";
        public string DefaultStudentImage { get; } = "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";
    }
}