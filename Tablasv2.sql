USE EcommerceDB;
GO

-- Tabla Categoria
CREATE TABLE Categoria (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255),
    Activa BIT NOT NULL DEFAULT 1
);
GO

-- Insert único para Categoria (7 categorías en un solo INSERT)
INSERT INTO Categoria (Nombre, Descripcion)
VALUES
('Hardware', 'Componentes físicos de computadoras'),
('Periféricos', 'Dispositivos de entrada y salida'),
('Almacenamiento', 'Discos y memorias'),
('Redes', 'Equipos y accesorios de red'),
('Electrónica', 'Productos electrónicos y tecnología'),
('Hogar', 'Artículos para el hogar'),
('Indumentaria', 'Ropa y accesorios');
GO

-- Tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Insert único para Usuario (3 usuarios en un solo INSERT)
INSERT INTO Usuario (Nombre, Email, Password, Rol)
VALUES
('Carlos Tech', 'carlos@informatica.com', '1234', 'Cliente'),
('Laura Sistemas', 'laura@sistemas.com', '1234', 'Cliente'),
('Admin IT', 'admin@informatica.com', 'admin123', 'Admin');
GO

-- Tabla Producto
CREATE TABLE Producto (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(500),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    ImagenUrl VARCHAR(255),
    IdCategoria INT NOT NULL,
    CONSTRAINT FK_Producto_Categoria
        FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria)
);
GO

-- Insert único para Producto (6 productos en un solo INSERT)
INSERT INTO Producto (Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria)
VALUES
('Mouse Gamer Logitech G203', 'Mouse óptico 8000 DPI RGB', 18500.00, 25, 'mouse_g203.jpg', 2),
('Teclado Mecánico Redragon Kumara', 'Teclado mecánico switch blue', 42000.00, 15, 'teclado_kumara.jpg', 2),
('Disco SSD Kingston 480GB', 'SSD SATA III 480GB', 52000.00, 20, 'ssd_kingston_480.jpg', 3),
('Memoria RAM Corsair 16GB DDR4', 'Memoria DDR4 3200MHz', 68000.00, 10, 'ram_corsair_16gb.jpg', 1),
('Router TP-Link Archer C6', 'Router WiFi AC1200', 75000.00, 8, 'router_archer_c6.jpg', 4),
('Placa de Video NVIDIA RTX 3060', 'GPU 12GB GDDR6', 420000.00, 4, 'rtx_3060.jpg', 1);
GO

-- Tabla Pedido
CREATE TABLE Pedido (
    IdPedido INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(30) NOT NULL,
    IdUsuario INT NOT NULL,
    CONSTRAINT FK_Pedido_Usuario
        FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
GO

-- Insert único para Pedido
INSERT INTO Pedido (Total, Estado, IdUsuario)
VALUES
(60500.00, 'Pendiente', 1);
GO

-- Tabla DetallePedido
CREATE TABLE DetallePedido (
    IdDetallePedido INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetallePedido_Pedido
        FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido),
    CONSTRAINT FK_DetallePedido_Producto
        FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
GO

-- Insert único para DetallePedido (2 detalles en un solo INSERT)
INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario)
VALUES
(1, 1, 1, 18500.00),
(1, 3, 1, 52000.00);
GO

-- Tabla Direccion
CREATE TABLE Direccion (
    IdDireccion INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Calle VARCHAR(150) NOT NULL,
    Numero VARCHAR(20) NOT NULL,
    Localidad VARCHAR(100) NOT NULL,
    CodigoPostal VARCHAR(10) NOT NULL,
    Observaciones VARCHAR(255),
    CONSTRAINT FK_Direccion_Usuario
        FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
GO
-- Tabla Forma de pago
CREATE TABLE FormaPago (
    IdFormaPago INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);
GO

-- Ejemplos
INSERT INTO FormaPago (Nombre, Descripcion)
VALUES
('Tarjeta de Crédito', 'Pago con tarjeta de crédito'),
('Transferencia Bancaria', 'Pago mediante transferencia bancaria'),
('MercadoPago', 'Pago online seguro');
GO

-- Tabla Forma de entrega
CREATE TABLE FormaEntrega (
    IdFormaEntrega INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);
GO

-- Ejemplos
INSERT INTO FormaEntrega (Nombre, Descripcion)
VALUES
('Retiro en local', 'Retiro del producto en la tienda física'),
('Envío a domicilio', 'Entrega a domicilio mediante mensajería');
GO

-- Ajuste en la tabla Pedido
ALTER TABLE Pedido
ADD IdFormaPago INT NULL,
    IdFormaEntrega INT NULL,
    IdDireccionEntrega INT NULL,
    CONSTRAINT FK_Pedido_FormaPago FOREIGN KEY (IdFormaPago) REFERENCES FormaPago(IdFormaPago),
    CONSTRAINT FK_Pedido_FormaEntrega FOREIGN KEY (IdFormaEntrega) REFERENCES FormaEntrega(IdFormaEntrega),
    CONSTRAINT FK_Pedido_DireccionEntrega FOREIGN KEY (IdDireccionEntrega) REFERENCES Direccion(IdDireccion);
GO


