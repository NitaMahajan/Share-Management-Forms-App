create proc adduser
@username varchar(50),
@password varchar(50)
as 
insert into [dbo].[user] values(@username,@password)