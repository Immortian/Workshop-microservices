CREATE TABLE IF NOT EXISTS public."User"
(
    user_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    user_name character varying(30) COLLATE pg_catalog."default" NOT NULL,
    user_email character varying(30) COLLATE pg_catalog."default" NOT NULL,
    user_password character varying(30) COLLATE pg_catalog."default" NOT NULL,
    is_vip boolean NOT NULL DEFAULT false,
    CONSTRAINT "User_pkey" PRIMARY KEY (user_id),
    CONSTRAINT "User_user_name_key" UNIQUE (user_name),
    CONSTRAINT "User_user_email_key" UNIQUE (user_email)
);

CREATE TABLE IF NOT EXISTS public."UserUpdateLog"
(
    log_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    user_id integer NOT NULL,
    is_created boolean NOT NULL DEFAULT true,
    is_name_updated boolean NOT NULL DEFAULT false,
    is_password_updated boolean NOT NULL DEFAULT false,
    is_vip_updated boolean NOT NULL DEFAULT false,
    log_datetime timestamp without time zone NOT NULL,
    CONSTRAINT "UserUpdateLog_pkey" PRIMARY KEY (log_id)
);

ALTER TABLE IF EXISTS public."UserUpdateLog"
	ADD FOREIGN KEY (user_id)
	REFERENCES public."User" (user_id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;