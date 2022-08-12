CREATE TABLE IF NOT EXISTS public."Transaction_info"
(
    transaction_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
	seller_id integer NOT NULL,
	buyer_id integer NOT NULL,
    collection_id integer,
    item_id integer,
    price money NOT NULL,
	transaction_datetime timestamp without time zone NOT NULL,
    CONSTRAINT "Transaction_info_pkey" PRIMARY KEY (transaction_id)
);