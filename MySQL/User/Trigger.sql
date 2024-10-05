use hospedapp;

DELIMITER $$
CREATE TRIGGER BefInsUser BEFORE INSERT ON User
FOR EACH ROW
BEGIN
    SET NEW.Pass = SHA2(NEW.Pass, 256);
END $$