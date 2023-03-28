create table admin(
	admin_id tinyint identity(1,1) primary key,
	first_name varchar(16) not null,
	last_name varchar(16) not null,
	email varchar(128) not null,
	password varchar(255) not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
)
create table banner(
	banner_id int identity(1,1) primary key,
	banner_image varchar(max) not null,
	title varchar(300) not null,
	description text,
	sort_order tinyint default 0,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
)

create table country(
	country_id smallint identity(1,1) primary key,
	country_name varchar(50) not null,
	ISO varchar(16),
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
)

create table city(
	city_id int identity(1,1) primary key,
	city_name varchar(50) not null,
	country_id smallint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (country_id) references country(country_id)
)

create table [user](
	user_id bigint identity(1,1) primary key,
	city_id int,
	country_id smallint,
	first_name varchar(16) not null,
	last_name varchar(16) not null,
	email varchar(128) not null,
	password varchar(255) not null,
	phone_number varchar(10) not null,
	avatar varchar(max),
	why_i_volunteer text,
	employee_id varchar(16),
	department varchar(16),
	profile_text text,
	linked_in_url varchar(255),
	title varchar(255),
	status bit not null default 1,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (city_id) references city(city_id),
	foreign key (country_id) references country(country_id)
)
create table reset_password(
	email varchar(128) primary key,
	token varchar(255) not null,
	created_at datetimeoffset not null default current_timestamp
)
create table cms_page(
	cms_page_id smallint identity(1,1) primary key,
	title varchar(255) not null,
	description text not null,
	slug varchar(255),
	status bit not null default 1,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
)
create table mission_theme(
	mission_theme_id smallint identity(1,1) primary key,
	mission_theme_name varchar(50) unique,
	status bit not null default 1,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
)

create table skill(
	skill_id smallint identity(1,1) primary key,
	skill_name varchar(50) not null unique,
	status bit not null default 1,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
)

create table user_skill(
	user_skill_id bigint identity(1,1) primary key,
	user_id bigint not null,
	skill_id smallint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset
	foreign key (user_id) references [user](user_id)
)

create table mission(
	mission_id bigint identity(1,1) primary key,
	mission_city int not null,
	mission_country smallint not null,
	mission_theme_id smallint not null,
	title varchar(255) not null,
	short_description text,
	description text,
	organization_name varchar(30) not null,
	organization_details text,
	start_date datetime2,
	end_date datetime2,
	mission_type bit not null,
	total_seats smallint,
	registration_deadline datetime2,
	availability tinyint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	mission_rating tinyint default 0,
	foreign key (mission_city) references city(city_id),
	foreign key (mission_country) references country(country_id),
	foreign key (mission_theme_id) references mission_theme(mission_theme_id)
)
create table mission_skill(
	mission_skill_id bigint identity(1,1) primary key,
	skill_id smallint not null,
	mission_id bigint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (skill_id) references skill(skill_id)
)
create table mission_media(
	mission_media_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	media_name varchar(64),
	media_type varchar(5),
	media_path varchar(max),
	[default] bit default 0,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id)
)
create table mission_document(
	mission_document_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	document_name varchar(64),
	document_type varchar(5),
	document_path varchar(max),
	title varchar(100),
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id)
)
create table mission_application(
	mission_application_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	user_id bigint not null,
	applied_at datetime2 not null,
	approval_status tinyint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (user_id) references [user](user_id)
)

create table favourite_mission(
	favourite_mission_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	user_id bigint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (user_id) references [user](user_id)
)
create table mission_rating(
	mission_rating_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	user_id bigint not null,
	rating tinyint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (user_id) references [user](user_id)
)
create table mission_goal(
	mission_goal_id bigint identity(1,1) primary key,
	mission_id bigint,
	goal_objective varchar(255),
	goal_value int,
	goal_achieved int,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
)

create table story(
	story_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	user_id bigint not null,
	title varchar(80) not null,
	published_at datetimeoffset,
	approval_status tinyint not null default 0,
	description text not null,
	short_description varchar(150) not null,
	video_url text,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (user_id) references [user](user_id)
)

create table comment(
	comment_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	user_id bigint not null,
	approval_status tinyint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (user_id) references [user](user_id)
)

create table mission_invite(
	mission_invite_id bigint identity(1,1) primary key,
	mission_id bigint not null,
	from_user_id bigint not null,
	to_user_id bigint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (mission_id) references mission(mission_id),
	foreign key (from_user_id) references [user](user_id),
	foreign key (to_user_id) references [user](user_id)
)

create table story_invite(
	story_invite_id bigint identity(1,1) primary key,
	story_id bigint not null,
	from_user_id bigint not null,
	to_user_id bigint not null,
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (story_id) references story(story_id),
	foreign key (from_user_id) references [user](user_id),
	foreign key (to_user_id) references [user](user_id)
)

create table story_media(
	story_media_id bigint identity(1,1) primary key,
	story_id bigint not null,
	media_name varchar(64),
	media_type varchar(5),
	media_path varchar(max),
	created_at datetimeoffset not null default current_timestamp,
	updated_at datetimeoffset,
	deleted_at datetimeoffset,
	foreign key (story_id) references story(story_id)
)