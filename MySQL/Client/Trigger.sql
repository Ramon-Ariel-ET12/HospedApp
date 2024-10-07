#######################################################################################
DELIMITER $$
DROP TRIGGER IF EXISTS befInsClient $$
CREATE TRIGGER befInsClient BEFORE INSERT ON Client
FOR EACH ROW
BEGIN
    SET NEW.Pass = SHA2(NEW.Pass, 256);
	IF (CHAR_LENGTH(NEW.Dni) != 8)THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'El dni tiene que ser de 8 digitos';
	END IF;
END $$
#######################################################################################
