CREATE TABLE IF NOT EXISTS public."Item"
(
    item_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    item_name character varying(30) COLLATE pg_catalog."default" NOT NULL,
    item_type character varying(30) COLLATE pg_catalog."default" NOT NULL,
	item_price money NOT NULL,
    item_description character varying(150) COLLATE pg_catalog."default",
    item_collection_id integer,
    item_owner_id integer NOT NULL,
    CONSTRAINT "Item_pkey" PRIMARY KEY (item_id)
);

CREATE TABLE IF NOT EXISTS public."ItemCollection"
(
    collection_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    collection_owner_id integer NOT NULL,
	collection_name character varying(30) COLLATE pg_catalog."default",
	collection_price money NOT NULL,
    collection_description character varying(150) COLLATE pg_catalog."default",
    CONSTRAINT "ItemCollection_pkey" PRIMARY KEY (collection_id)
);

ALTER TABLE IF EXISTS public."Item"
    ADD FOREIGN KEY (item_collection_id)
    REFERENCES public."ItemCollection" (collection_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;
	
CREATE TABLE IF NOT EXISTS public."ItemUpdateLog"
(
	log_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    item_id integer NOT NULL,
	is_created boolean NOT NULL DEFAULT true,
    is_name_updated boolean NOT NULL DEFAULT false,
	is_type_updated boolean NOT NULL DEFAULT false,
	is_price_updated boolean NOT NULL DEFAULT false,
	is_description_updated boolean NOT NULL DEFAULT false,
	is_collection_id_updated boolean NOT NULL DEFAULT false,
    log_datetime timestamp without time zone NOT NULL,
	CONSTRAINT "ItemUpdateLog_pkey" PRIMARY KEY (log_id)
);

ALTER TABLE IF EXISTS public."ItemUpdateLog"
	ADD FOREIGN KEY (item_id)
	REFERENCES public."Item" (item_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;
	
CREATE TABLE IF NOT EXISTS public."ItemCollectionUpdateLog"
(
	log_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    collection_id integer NOT NULL,
	is_created boolean NOT NULL DEFAULT true,
    is_name_updated boolean NOT NULL DEFAULT false,
	is_price_updated boolean NOT NULL DEFAULT false,
	is_description_updated boolean NOT NULL DEFAULT false,
	is_owner_updated boolean NOT NULL DEFAULT false,
    log_datetime timestamp without time zone NOT NULL,
	CONSTRAINT "ItemCollectionUpdateLog_pkey" PRIMARY KEY (log_id)
);

ALTER TABLE IF EXISTS public."ItemCollectionUpdateLog"
	ADD FOREIGN KEY (collection_id)
	REFERENCES public."ItemCollection" (collection_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;