CREATE TABLE Clientes ( ClienteId INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre VARCHAR(60),
                        Email VARCHAR(60),
                        Telefono VARCHAR(10)
                        );

ALTER TABLE Presupuestos ADD ClienteId INT;

DROP TABLE Clientes;
SELECT * FROM Presupuestos;

-- Por Las dudas creo un backup de la tabla Presupuestos
CREATE TABLE Presupuestos_viejo AS
SELECT * FROM Presupuestos;

SELECT * FROM Presupuestos_viejo;

-- En SQlite no se puede agregar una restriccion de clave foranea cuando la tabla ya a sido creada por lo tanto la voy a crear de nuevo
CREATE TABLE PresupuestosNuevo ( idPresupuesto INT AUTO_INCREMENT,
                                 ClienteID INT NOT NULL,
                                 FechaCreacion DATE NOT NULL,
                                 CONSTRAINT pk_Presupuestos PRIMARY KEY (idPresupuesto),
                                 CONSTRAINT fk_Clientes FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
                                );
                                
-- Borro la tabla Presupuestos
DROP TABLE Presupuestos;

-- Le cambio el nombre a la que cree
ALTER TABLE PresupuestosNuevo RENAME TO Presupuestos;

SELECT * FROM Clientes INNER JOIN Presupuestos USING(ClienteId);

INSERT INTO Clientes(Nombre,Email,Telefono) VALUES ('Hola', 'asdsda','8112');
