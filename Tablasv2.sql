USE EcommerceDB;
GO

/* =========================
   TABLA CATEGORIA
========================= */
CREATE TABLE Categoria (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255),
    Activa BIT NOT NULL DEFAULT 1
);
GO

/* =========================
   TABLA USUARIO
========================= */
CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);
GO

/* =========================
   TABLA DIRECCION
========================= */
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

/* =========================
   TABLA PRODUCTO
========================= */
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

/* =========================
   TABLA FORMA DE PAGO
========================= */
CREATE TABLE FormaPago (
    IdFormaPago INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);
GO

/* =========================
   TABLA FORMA DE ENTREGA
========================= */
CREATE TABLE FormaEntrega (
    IdFormaEntrega INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255)
);
GO

/* =========================
   TABLA PEDIDO
========================= */
CREATE TABLE Pedido (
    IdPedido INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(30) NOT NULL,
    IdUsuario INT NOT NULL,
    IdDireccionEntrega INT NULL,
    IdFormaPago INT NULL,
    IdFormaEntrega INT NULL,
    CONSTRAINT FK_Pedido_Usuario
        FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Pedido_Direccion
        FOREIGN KEY (IdDireccionEntrega) REFERENCES Direccion(IdDireccion),
    CONSTRAINT FK_Pedido_FormaPago
        FOREIGN KEY (IdFormaPago) REFERENCES FormaPago(IdFormaPago),
    CONSTRAINT FK_Pedido_FormaEntrega
        FOREIGN KEY (IdFormaEntrega) REFERENCES FormaEntrega(IdFormaEntrega)
);
GO

/* =========================
   TABLA DETALLE PEDIDO
========================= */
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

/* =========================
   DATOS DE PRUEBA
========================= */

INSERT INTO Categoria (Nombre, Descripcion) VALUES
('Hardware', 'Componentes físicos de computadoras'),
('Periféricos', 'Dispositivos de entrada y salida'),
('Almacenamiento', 'Discos y memorias'),
('Redes', 'Equipos y accesorios de red'),
('Electrónica', 'Productos electrónicos y tecnología'),
('Hogar', 'Artículos para el hogar'),
('Indumentaria', 'Ropa y accesorios');
GO

INSERT INTO Usuario (Nombre, Email, Password, Telefono, Rol) VALUES
('Carlos Tech', 'carlos@informatica.com', '1234', '123', 'Cliente'),
('Laura Sistemas', 'laura@sistemas.com', '1234', '456', 'Cliente'),
('Admin IT', 'admin@informatica.com', 'admin123', '789', 'Admin');
GO

INSERT INTO FormaPago (Nombre, Descripcion) VALUES
('Tarjeta de Crédito', 'Pago con tarjeta de crédito'),
('Transferencia Bancaria', 'Pago mediante transferencia bancaria'),
('MercadoPago', 'Pago online seguro');
GO

INSERT INTO FormaEntrega (Nombre, Descripcion) VALUES
('Retiro en local', 'Retiro del producto en la tienda física'),
('Envío a domicilio', 'Entrega a domicilio mediante mensajería');
GO

INSERT INTO Producto (Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria) VALUES
('Mouse Gamer Logitech G203', 'Mouse óptico 8000 DPI RGB', 18500, 25, 'mouse_g203.jpg', 2),
('Teclado Mecánico Redragon Kumara', 'Teclado mecánico switch blue', 42000, 15, 'teclado_kumara.jpg', 2),
('Disco SSD Kingston 480GB', 'SSD SATA III 480GB', 52000, 20, 'ssd_kingston_480.jpg', 3),
('Memoria RAM Corsair 16GB DDR4', 'Memoria DDR4 3200MHz', 68000, 10, 'ram_corsair_16gb.jpg', 1),
('Router TP-Link Archer C6', 'Router WiFi AC1200', 75000, 8, 'router_archer_c6.jpg', 4),
('Placa de Video NVIDIA RTX 3060', 'GPU 12GB GDDR6', 420000, 4, 'rtx_3060.jpg', 1);
GO
