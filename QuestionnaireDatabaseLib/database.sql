create table "Role"
(
    "Name" varchar primary key
);

create table "Class"
(
    "Name" varchar primary key
);

create table "Account"
(
    "Login"      varchar primary key,
    "Password"   varchar not null,
    "FirstName"  varchar not null,
    "LastName"   varchar not null,
    "Patronymic" varchar not null,
    "Role"       varchar not null references "Role" ("Name"),
    "Class"      varchar references "Class" ("Name")
);

create table "Form"
(
    "ID"      serial primary key,
    "Name"    varchar not null,
    "Teacher" varchar not null references "Account" ("Login")
);

create table "QuestionType"
(
    "Name"        varchar primary key,
    "Description" varchar not null
);

create table "Question"
(
    "ID"       serial primary key,
    "Type"     varchar references "QuestionType" ("Name"),
    "Content"  jsonb   not null,
    "Form"     integer not null references "Form" ("ID"),
    "Position" integer not null
);

create table "Answer"
(
    "ID"       serial,
    "Student"  varchar references "Account" ("Login"),
    "Question" integer references "Question" ("ID"),
    "Content"  jsonb     not null,
    "Date"     timestamp not null
);

INSERT INTO "Role" ("Name") VALUES ('Admin');
INSERT INTO "Role" ("Name") VALUES ('Teacher');
INSERT INTO "Role" ("Name") VALUES ('Student');

INSERT INTO "QuestionType" ("Name", "Description") VALUES ('TextBox', 'Поле ввода');
INSERT INTO "QuestionType" ("Name", "Description") VALUES ('DatePicker', 'Выбор даты');
INSERT INTO "QuestionType" ("Name", "Description") VALUES ('ComboBox', 'Выбор одного варианта из списка');
INSERT INTO "QuestionType" ("Name", "Description") VALUES ('CheckBox', 'Выбор нескольких вариантов из списка');
