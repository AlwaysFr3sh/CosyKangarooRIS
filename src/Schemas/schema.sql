CREATE TABLE IF NOT EXISTS item (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	name TEXT UNIQUE NOT NULL,
	price DECIMAL NOT NULL
);

CREATE TABLE IF NOT EXISTS person (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	username TEXT UNIQUE NOT NULL,
	password TEXT NOT NULL,
	phone TEXT NOT NULL,
	address TEXT NOT NULL,
  employee NUMERIC NOT NULL
);

CREATE TABLE IF NOT EXISTS payment (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	amount DECIMAL NOT NULL,
	method TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS reservations (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	customerName TEXT NOT NULL REFERENCES person(username),
	patrons INTEGER NOT NULL,
	resDate TEXT NOT NULL,
	resTime TEXT NOT NULL
);


CREATE TABLE IF NOT EXISTS TableDetails (
	SaleID INTEGER PRIMARY KEY AUTOINCREMENT,
	TableNumber TEXT NOT NULL,
	patrons INTEGER NOT NULL,
	TableDate TEXT NOT NULL,
	TableTime TEXT NOT NULL
);

-- One-to-Many Relationship between TableDetails and orders
-- Manys orders are possible for the single table
CREATE TABLE IF NOT EXISTS orders (
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	TableNumber TEXT NOT NULL REFERENCES TableDetails(TableNumber),
	itemid INTEGER NOT NULL REFERENCES item(id),
	quantity INTEGER NOT NULL,
	kitchenstatus TEXT NOT NULL
);
