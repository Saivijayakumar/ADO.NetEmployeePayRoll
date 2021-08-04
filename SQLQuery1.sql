create procedure UpdateBasePay
@Id int,
@Name varchar(50),
@Base_pay float
as
begin
update employee_payroll set Base_Pay=@Base_pay where Id=@Id and Name=@Name;
end;