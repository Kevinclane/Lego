USE legokits1;

-- CREATE TABLE legos(
--   id INT AUTO_INCREMENT,
--   name VARCHAR(255) NOT NULL,
--   size VARCHAR(255) NOT NULL,
--   owner VARCHAR(255) NOT NULL,
--   PRIMARY KEY (id)
-- )
-- CREATE TABLE kits(
--   id INT AUTO_INCREMENT,
--   name VARCHAR(255) NOT NULL,
--   piececount DECIMAL(10,2) NOT NULL,
--   price DECIMAL(7,2) NOT NULL,
--    owner VARCHAR(255) NOT NULL,
--   PRIMARY KEY (id)
-- )

-- CREATE TABLE legokits(
--   id int NOT NULL AUTO_INCREMENT,
--   kitId int NOT NULL,
--   legoId int NOT NULL,
--   PRIMARY KEY(id),
--   FOREIGN KEY (kitId)
--     REFERENCES kits (id)
--     ON DELETE CASCADE,
--   FOREIGN KEY (legoId)
--     REFERENCES legos (id)
--     ON DELETE CASCADE
-- )

-- DROP TABLE legos