create table authors
(id number(20,0) primary key not null,
firstname nvarchar2(50) not null,
lastname nvarchar2(50) not null
)
/
create sequence seq_authors start with 1;
/
create table publishers
(id number(20,0) primary key not null,
publisher_title nvarchar2(50) not null
)
/
create sequence seq_publishers start with 1;
/

create table books
(id number(20,0) primary key not null,
description nvarchar2(50) not null,
isbn nvarchar2(50) not null,
penalty_price float(126) not null,
publisher_id number(20,0) not null,
constraint fk_publisher_id foreign key(publisher_id) references publishers(id)
)
/
create sequence seq_books start with 1;
/

create table inventoryitems
(id number(20,0) primary key not null,
is_ordered number(1,0),
is_readcarted number(1,0),
book_id number(20,0) not null,
inventory_code nvarchar2(255),
constraint fk_book_inventoryitem foreign key(book_id)references books(id)
)
/
create sequence seq_inventoryitems start with 1; 
/
create table books_to_authors
(
id number(20,0) primary key not null,
pos_authorlist number(20,0) not null,
book_id number(20,0) not null,
author_id number(20,0) not null,
constraint fk_author foreign key (author_id) references authors(id),
constraint fk_book foreign key (book_id) references books(id)
)

/
create sequence seq_books_to_authors start with 1;
/


create table reader
(reader_id number(20,0) primary key not null,
email_address nvarchar2(255) not null
)
/
create sequence seq_reader start with 1;
/

create table approvedorders
(id number(20,0) primary key not null,
approvednumber nvarchar2(255),
ordereddate timestamp(4),
recovereddate timestamp(4),
reader_id number(10,0),
position number(10,0),
constraint fk_approvedorder_reader foreign key(reader_id) references reader(reader_id)
)
/
create sequence seq_approved start with 1; 
/

create table readercart
(id number(20,0) primary key not null,
cart_number nvarchar2(255) not null,
reader_id number(20,0) not null,
constraint fk_readercar_reader foreign key(reader_id) references reader(reader_id)
)
/
create sequence seq_reader_cart start with 1; 
/

create table items
(id number(20,0) primary key not null,
is_ordered number(1,0),
is_readcarted number(1,0),
inventory_serialcode nvarchar2(255),
recovereddate TIMESTAMP (4),
planedrecoveringdate TIMESTAMP(4),
approvedorder_id number(20,0),
book_id number(20,0) not null,
constraint fk_approved_item foreign key(approvedorder_id) references approvedorders(id),
constraint fk_book_item foreign key(book_id)references books(id)
)
/
create sequence seq_items_ start with 1; 
/
create table readercartselections
(id number(20,0) not null,
book_id number(20,0) not null,
readingcart_id number(20,0) not null,
item_id number(20,0)not null,
primary key(id),
constraint fk_cartselect_book foreign key(book_id) references books(id),
constraint fk_cartselect_readingcart foreign key(readingcart_id)references readercart(id),
constraint fk_cartselect_item foreign key(item_id) references items(id)
)
/
create sequence seq_cart_selection start with 1;
/