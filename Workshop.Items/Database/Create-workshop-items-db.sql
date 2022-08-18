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
    item_count integer NOT NULL,
    CONSTRAINT "ItemCollection_pkey" PRIMARY KEY (collection_id)
);

ALTER TABLE IF EXISTS public."Item"
    ADD FOREIGN KEY (item_collection_id)
    REFERENCES public."ItemCollection" (collection_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;
	