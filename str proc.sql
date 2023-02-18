create table Mens (
Id bigint not null identity(0,1) primary key,
Identifier uniqueidentifier not null,
CName Nvarchar(Max),
CImage nvarchar(Max),
CRate Nvarchar(100),
Category int not null,
UsersId  uniqueidentifier null,
)


drop table mens


create proc sp_men_add
@Identifier uniqueidentifier,
@Name nvarchar(MAX),
@Image nvarchar(max),
@Rate nvarchar(Max),
@category int,
@userId uniqueidentifier
as
begin
insert into Mens values(
@Identifier,
@Name,
@Image,
@Rate,
@category,
@userId
)
end

create proc sp_getAll
as
begin
    select * from mens
end


create proc sp_getbyGuidId
@guid uniqueidentifier
as
begin
   select * from Mens where Identifier = @guid
end

create proc sp_getbyId
@id bigint
as
begin
   select * from Mens where Id = @id
end


exec sp_getbyId @id=0
exec sp_getbyGuidId @guid = 'B2A3846D-A532-46AA-B9DB-2B5C3CA86312'


 
create proc sp_UpdateMens
@id bigint,
@Name nvarchar(MAX),
@Image nvarchar(max),
@Rate nvarchar(Max),
@category int,
@userId uniqueidentifier
as
begin
   update Mens Set 
   CName =iif(isnull(@Name,'') <> '', @Name, CName),
   CImage = iif(isnull(@Image,'') <> '', @Image, CImage),
   CRate = iif(isnull( @Rate,'') <> '',  @Rate, CRate),
   Category = iif(isnull( @category,'') <> '',  @category, Category)
   where id = @id
end