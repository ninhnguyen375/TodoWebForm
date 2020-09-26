use todo;

drop table usersTasks;
drop table comments;
drop table tasks;
drop table users;

go

create table users (
	id int not null identity(1, 1) primary key,
	name nvarchar(255) not null,
	email nvarchar(255) not null, 
	password nvarchar(255) not null,
	role nvarchar(255) not null,
)

go

create table tasks (
	id int not null identity(1, 1) primary key,
	title nvarchar(255) not null,
	startDate datetime not null,
	endDate datetime not null,
	status nvarchar(255)  not null,
	private tinyint not null,
	urlFile nvarchar(255),
)

go

create table usersTasks (
	userId int not null references users(id),
	taskId int not null references tasks(id),
	isOwner tinyint not null,
	constraint pk_usersTasks primary key (userId, taskId)
)	

go

create table comments(
	id int not null identity(1, 1) primary key,
	userId int not null references users(id) ,
	taskId int not null references tasks(id),
	content nvarchar(255) not null,
	isRemind tinyint not null
)

go

insert into users (
	name, email, password, role
) values ('Le Minh Cuong','leminhcuong2988@yahoo.com.vn', '12345678','admin');

go 

insert into users (
	name, email, password, role
) values ('Nguyen An Ninh','ninh@yahoo.com.vn', '12345678','employee');

insert into users (
	name, email, password, role
) values ('Ngu Duc Thuan','thuan@yahoo.com.vn', '12345678','admin');


insert into users (
	name, email, password, role
) values ('Nguyen Sang','sang@yahoo.com.vn', '12345678','employee');

insert into users (
	name, email, password, role
) values ('Diem','diem@yahoo.com.vn', '12345678','admin');