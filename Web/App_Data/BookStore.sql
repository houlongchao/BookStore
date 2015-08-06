/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2015/8/6 17:34:20                            */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Book') and o.name = 'FK_BOOK_REFERENCE_CATEGORY')
alter table Book
   drop constraint FK_BOOK_REFERENCE_CATEGORY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Cart') and o.name = 'FK_CART_REFERENCE_CUSTOMER')
alter table Cart
   drop constraint FK_CART_REFERENCE_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CartItem') and o.name = 'FK_CARTITEM_REFERENCE_CART')
alter table CartItem
   drop constraint FK_CARTITEM_REFERENCE_CART
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CartItem') and o.name = 'FK_CARTITEM_REFERENCE_BOOK')
alter table CartItem
   drop constraint FK_CARTITEM_REFERENCE_BOOK
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Orders') and o.name = 'FK_ORDERS_REFERENCE_CUSTOMER')
alter table Orders
   drop constraint FK_ORDERS_REFERENCE_CUSTOMER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrdersItem') and o.name = 'FK_ORDERSIT_REFERENCE_ORDERS')
alter table OrdersItem
   drop constraint FK_ORDERSIT_REFERENCE_ORDERS
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('OrdersItem') and o.name = 'FK_ORDERSIT_REFERENCE_BOOK')
alter table OrdersItem
   drop constraint FK_ORDERSIT_REFERENCE_BOOK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Book')
            and   type = 'U')
   drop table Book
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Cart')
            and   type = 'U')
   drop table Cart
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CartItem')
            and   type = 'U')
   drop table CartItem
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Category')
            and   type = 'U')
   drop table Category
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Customer')
            and   type = 'U')
   drop table Customer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FriendLink')
            and   type = 'U')
   drop table FriendLink
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Orders')
            and   type = 'U')
   drop table Orders
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrdersItem')
            and   type = 'U')
   drop table OrdersItem
go

/*==============================================================*/
/* Table: Book                                                  */
/*==============================================================*/
create table Book (
   id                   int                  identity,
   name                 varchar(200)         null,
   author               varchar(50)          null,
   description          text                 null,
   categoryId           int                  null,
   imageUrl             varchar(200)         null,
   price                decimal(10,2)        null,
   pageview             bigint               null,
   sale                 bigint               null,
   stock                bigint               null,
   constraint PK_BOOK primary key (id)
)
go

/*==============================================================*/
/* Table: Cart                                                  */
/*==============================================================*/
create table Cart (
   id                   int                  identity,
   price                decimal(10,2)        null,
   num                  int                  null,
   customerId           int                  null,
   constraint PK_CART primary key (id)
)
go

/*==============================================================*/
/* Table: CartItem                                              */
/*==============================================================*/
create table CartItem (
   id                   int                  not null,
   num                  int                  null,
   price                decimal(10,2)        null,
   bookId               int                  null,
   cartId               int                  null,
   constraint PK_CARTITEM primary key (id)
)
go

/*==============================================================*/
/* Table: Category                                              */
/*==============================================================*/
create table Category (
   id                   int                  identity,
   name                 varchar(200)         null,
   description          varchar(500)         null,
   constraint PK_CATEGORY primary key (id)
)
go

/*==============================================================*/
/* Table: Customer                                              */
/*==============================================================*/
create table Customer (
   id                   int                  identity,
   username             varchar(200)         null,
   password             varchar(200)         null,
   sex                  varchar(20)          null,
   telephone            varchar(200)         null,
   description          varchar(500)         null,
   address              varchar(200)         null,
   email                varchar(200)         null,
   actived              int                  null,
   code                 varchar(200)         null,
   role                 int                  null,
   constraint PK_CUSTOMER primary key (id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'actived')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'actived'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1-已经激活
   0-尚未激活',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'actived'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Customer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'role')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Customer', 'column', 'role'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1-是
   0-否',
   'user', @CurrentUser, 'table', 'Customer', 'column', 'role'
go

/*==============================================================*/
/* Table: FriendLink                                            */
/*==============================================================*/
create table FriendLink (
   id                   int                  identity,
   name                 varchar(200)         null,
   url                  varchar(200)         null,
   img                  varchar(200)         null,
   sort                 int                  null,
   constraint PK_FRIENDLINK primary key (id)
)
go

/*==============================================================*/
/* Table: Orders                                                */
/*==============================================================*/
create table Orders (
   id                   int                  not null,
   ordernum             varchar(200)         null,
   price                decimal(10,2)        null,
   num                  int                  null,
   status               int                  null,
   customer             int                  null,
   receiverName         varchar(200)         null,
   receiverPhone        varchar(200)         null,
   receiverAddress      varchar(500)         null,
   constraint PK_ORDERS primary key (id)
)
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Orders')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Orders', 'column', 'status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0-未付款
   1-已付款
   ',
   'user', @CurrentUser, 'table', 'Orders', 'column', 'status'
go

/*==============================================================*/
/* Table: OrdersItem                                            */
/*==============================================================*/
create table OrdersItem (
   id                   int                  not null,
   bookId               int                  null,
   price                decimal(10,2)        null,
   num                  int                  null,
   ordersId             int                  null,
   constraint PK_ORDERSITEM primary key (id)
)
go

alter table Book
   add constraint FK_BOOK_REFERENCE_CATEGORY foreign key (categoryId)
      references Category (id)
go

alter table Cart
   add constraint FK_CART_REFERENCE_CUSTOMER foreign key (customerId)
      references Customer (id)
go

alter table CartItem
   add constraint FK_CARTITEM_REFERENCE_CART foreign key (cartId)
      references Cart (id)
go

alter table CartItem
   add constraint FK_CARTITEM_REFERENCE_BOOK foreign key (bookId)
      references Book (id)
go

alter table Orders
   add constraint FK_ORDERS_REFERENCE_CUSTOMER foreign key (customer)
      references Customer (id)
go

alter table OrdersItem
   add constraint FK_ORDERSIT_REFERENCE_ORDERS foreign key (ordersId)
      references Orders (id)
go

alter table OrdersItem
   add constraint FK_ORDERSIT_REFERENCE_BOOK foreign key (bookId)
      references Book (id)
go

