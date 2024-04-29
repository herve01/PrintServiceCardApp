create database if not exists print_service_card_db;
use print_service_card_db;

create table print_service_card_db_info(
	param varchar(100) not null,
    valeur mediumtext not null,
    primary key(param)
);

create table if not exists province(
	id int auto_increment,
	nom VARCHAR(30),
	created_at datetime,
	constraint pk_province PRIMARY KEY(id)
);

/*Ville ou territoire*/
create table if not exists zone(
	id INT auto_increment,
    type varchar(11),
	nom VARCHAR(50),
	province_id INT,
	created_at datetime,
	constraint pk_zone PRIMARY KEY(id),
	constraint fk_zone_province FOREIGN KEY(province_id) REFERENCES province(id) on update cascade
);

create table if not exists grade(
	id varchar(6),
	intitule varchar(50),
	type varchar(15), /* Agent, Cadre, Haut-cadre*/
	niveau int default 0,
	description text,
	created_at datetime,
    updated_at datetime,
	constraint pk_grade primary key(id)
);

create table if not exists fonction(
	id varchar(32),
	grade_id varchar(6) not null,
	intitule varchar(100) not null,
	description text,
	created_at datetime,
    updated_at datetime,
	constraint pk_fonction primary key(id),
	constraint fk_fonction_grade foreign key(grade_id) references grade(id) on update cascade
);

create table if not exists personnel(
	id varchar(32),
	matricule varchar(20) unique,
	nom varchar(30) not null,
	postnom varchar(30),
	prenom varchar(30),
	sexe enum('Homme', 'Femme') not null,
    photo mediumblob,
	telephone VARCHAR(14),
    numero_qr_code varchar(15),
	created_at datetime,
    updated_at datetime,
	constraint pk_personnel primary key(id)
);

create table if not exists personnel_grade(
	id varchar(32),
	personnel_id varchar(32) not null,
	grade_id varchar(6) not null,
	date date,
	created_at datetime,
    updated_at datetime,
	constraint pk_personnel_grade primary key(id),
	constraint fk_personnel_grade_personnel foreign key(personnel_id) references personnel(id) on update cascade,
	constraint fk_personnel_grade_grade foreign key(grade_id) references grade(id) on update cascade
);

create table if not exists personnel_fonction(
	id varchar(32),
	personnel_id varchar(32) not null,
	fonction_id varchar(32) not null,
	date date,
	created_at datetime,
    updated_at datetime,
	constraint pk_personnel_fonction primary key(id),
	constraint fk_personnel_fonction_personnel foreign key(personnel_id) references personnel(id) on update cascade,
	constraint fk_personnel_fonction_fonction foreign key(fonction_id) references fonction(id) on update cascade
);

create table if not exists affectation(
	id varchar(32),
	personnel_id varchar(32) not null,
	zone_id int,
	date date,
	created_at datetime,
    updated_at datetime,
	constraint pk_affectation primary key(id),
	constraint fk_affectation_personnel foreign key(personnel_id) references personnel(id) on update cascade,
	constraint fk_affectation_zone foreign key(zone_id) references zone(id) on update cascade
);


delimiter %
drop PROCEDURE if exists get_carte_services%
CREATE  PROCEDURE get_carte_services(IN v_zone_id int)
BEGIN    
	select P.matricule, P.nom, P.postnom, P.prenom, P.sexe,
	G.id grade, F.intitule fonction, Z.nom affectation,
	P.photo, P.numero_qr_code labelQr
	 from
	personnel P inner join personnel_grade PG
	on P.id = PG.personnel_id
	inner join grade G 
	on G.id = PG.grade_id
	inner join personnel_fonction PF
	on P.id = PF.personnel_id
	inner join fonction F 
	on F.id = PF.fonction_id
	inner join affectation A 
	on P.id = A.personnel_id
	inner join zone Z 
	on Z.id = A.zone_id
    where Z.id = v_zone_id;
END%

delimiter %
drop PROCEDURE if exists get_carte_personnel_services%
CREATE  PROCEDURE get_carte_personnel_services(IN v_personnel_id varchar(32))
BEGIN    
	select P.matricule, P.nom, P.postnom, P.prenom, P.sexe,
	G.id grade, F.intitule fonction, Z.nom affectation,
	P.photo, P.numero_qr_code labelQr
	 from
	personnel P inner join personnel_grade PG
	on P.id = PG.personnel_id
	inner join grade G 
	on G.id = PG.grade_id
	inner join personnel_fonction PF
	on P.id = PF.personnel_id
	inner join fonction F 
	on F.id = PF.fonction_id
	inner join affectation A 
	on P.id = A.personnel_id
	inner join zone Z 
	on Z.id = A.zone_id
    where P.id = v_personnel_id;
END%