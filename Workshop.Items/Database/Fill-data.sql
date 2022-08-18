
INSERT INTO public."ItemCollection" (collection_owner_id, collection_name, collection_price, collection_description, item_count)
VALUES(	1, 'Past Time', 350.00,	'from past to future', 2);

INSERT INTO public."Item" (item_name, item_type, item_price, item_description, item_collection_id, item_owner_id)
VALUES('Alone entity', 'Painting', 150.00, 'Poor fantasy', 1, 1),
	  ('Happy friends', 'Painting', 200.00,	'That time i was happy', 1,	1);


