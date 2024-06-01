CREATE DATABASE SuperMas;
GO

USE SuperMas;
GO

-- Tabla Categoria
CREATE TABLE Categoria (
    ID INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL
);

-- Tabla Proveedores
CREATE TABLE Proveedores (
    IdProveedor INT IDENTITY(1,1) PRIMARY KEY,
    Imagen VARCHAR(255),
    NumeroDocumento VARCHAR(50),
    RazonSocial VARCHAR(255),
    Correo VARCHAR(255),
    Direccion VARCHAR(255),
    Telefono VARCHAR(50),
    Impuesto DECIMAL(5, 2)
);

-- Tabla Producto
CREATE TABLE Producto (
    ID INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    CategoriaID INT,
    Precio DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CategoriaID) REFERENCES Categoria(ID)
);

-- Tabla Inventario
CREATE TABLE Inventario (
    ID INT PRIMARY KEY IDENTITY,
    ProductoID INT,
    Cantidad INT NOT NULL,
    ProveedorID INT,
    FOREIGN KEY (ProductoID) REFERENCES Producto(ID),
    FOREIGN KEY (ProveedorID) REFERENCES Proveedores(IdProveedor)
);

-- Tabla Ventas
CREATE TABLE Ventas (
    ID INT PRIMARY KEY IDENTITY,
    Fecha DATE NOT NULL
);

-- Tabla ProductoVenta
CREATE TABLE ProductoVenta (
    ID INT PRIMARY KEY IDENTITY,
    ProductoID INT,
    VentaID INT,
    Cantidad INT NOT NULL,
    FOREIGN KEY (ProductoID) REFERENCES Producto(ID),
    FOREIGN KEY (VentaID) REFERENCES Ventas(ID)
);

-- Tabla Factura
CREATE TABLE Factura (
    ID INT PRIMARY KEY IDENTITY,
    VentasID INT,
    TotalAPagar DECIMAL(10, 2) NOT NULL,
    MontoCancelado DECIMAL(10, 2) NOT NULL,
    Cambio DECIMAL(10, 2),
    TipoDePago VARCHAR(50) NOT NULL,
    FOREIGN KEY (VentasID) REFERENCES Ventas(ID)
);

-- Tabla Roles
CREATE TABLE Roles (
    ID INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL
);

-- Tabla Empleados
CREATE TABLE Empleados (
    ID INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    FechaIngreso DATE NOT NULL,
    Salario DECIMAL(10, 2) NOT NULL,
    DiasTrabajados INT NOT NULL
);

-- Tabla Usuarios
CREATE TABLE Usuarios (
    ID INT PRIMARY KEY IDENTITY,
    RolID INT,
    EmpleadoID INT,
    Usuario VARCHAR(100) NOT NULL,
    Contrasena VARCHAR(100) NOT NULL,
    FOREIGN KEY (RolID) REFERENCES Roles(ID),
    FOREIGN KEY (EmpleadoID) REFERENCES Empleados(ID)
);
