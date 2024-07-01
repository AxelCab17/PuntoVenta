CREATE DATABASE SuperMas;
GO
USE [SuperMas]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[IdCarrito] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCarrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarritoDetalles]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarritoDetalles](
	[IdCarritoDetalle] [int] IDENTITY(1,1) NOT NULL,
	[IdCarrito] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCarritoDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleFacturas]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFacturas](
	[IdDetalle] [int] IDENTITY(1,1) NOT NULL,
	[IdFactura] [int] NULL,
	[IdProducto] [int] NULL,
	[NombreProducto] [nvarchar](100) NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[FechaIngreso] [date] NOT NULL,
	[HorasTrabajadas] [int] NOT NULL,
	[HorasRebajadas] [int] NOT NULL,
	[ValorPorHora] [decimal](10, 2) NOT NULL,
	[SalarioAjustado] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[IdFactura] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Subtotal] [decimal](10, 2) NULL,
	[IVA] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario](
	[IdInventario] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NULL,
	[Cantidad] [int] NOT NULL,
	[IdProveedor] [int] NULL,
	[NombreProducto] [varchar](50) NULL,
	[Categoria] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdInventario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[IdCategoria] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Imagen] [varchar](255) NULL,
	[NumeroDocumento] [varchar](50) NULL,
	[Nombre] [varchar](255) NULL,
	[Correo] [varchar](255) NULL,
	[Direccion] [varchar](255) NULL,
	[Telefono] [varchar](50) NULL,
	[Impuesto] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRol]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRol](
	[IdRol] [tinyint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](20) NULL,
	[Nombre] [varchar](50) NULL,
	[Correo] [varchar](100) NULL,
	[Contrasenna] [varchar](100) NULL,
	[IdRol] [int] NULL,
	[Estado] [int] NULL,
	[Token] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ventas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Carrito] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[CarritoDetalles]  WITH CHECK ADD FOREIGN KEY([IdCarrito])
REFERENCES [dbo].[Carrito] ([IdCarrito])
GO
ALTER TABLE [dbo].[CarritoDetalles]  WITH CHECK ADD FOREIGN KEY([IdCarrito])
REFERENCES [dbo].[Carrito] ([IdCarrito])
GO
ALTER TABLE [dbo].[CarritoDetalles]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[CarritoDetalles]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[DetalleFacturas]  WITH CHECK ADD  CONSTRAINT [FK_FacturaDetalle] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[Facturas] ([IdFactura])
GO
ALTER TABLE [dbo].[DetalleFacturas] CHECK CONSTRAINT [FK_FacturaDetalle]
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedores] ([IdProveedor])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedores] ([IdProveedor])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEmpleado]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEmpleado]
    @IdEmpleado INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaIngreso DATE,
    @HorasTrabajadas INT,
    @HorasRebajadas INT,
    @ValorPorHora DECIMAL(10, 2)
AS
BEGIN
    DECLARE @SalarioAjustado DECIMAL(10, 2);

    -- Calculando el salario ajustado
    SET @SalarioAjustado = (@HorasTrabajadas * @ValorPorHora) - (@HorasRebajadas * @ValorPorHora);

    UPDATE dbo.Empleados
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        FechaIngreso = @FechaIngreso,
        HorasTrabajadas = @HorasTrabajadas,
        HorasRebajadas = @HorasRebajadas,
        ValorPorHora = @ValorPorHora,
        SalarioAjustado = @SalarioAjustado
    WHERE IdEmpleado = @IdEmpleado;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProducto]
    @IdProducto INT,
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(18,2),
    @IdCategoria INT,
    @Stock INT
AS
BEGIN
    UPDATE Producto
    SET Nombre = @Nombre,
        Precio = @Precio,
        IdCategoria = @IdCategoria,
        Stock = @Stock
    WHERE IdProducto = @IdProducto
END


GO
/****** Object:  StoredProcedure [dbo].[ActualizarProveedor]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProveedor]
    @IdProveedor INT,
    @Imagen VARCHAR(255),
    @NumeroDocumento VARCHAR(50),
    @Nombre VARCHAR(255),
    @Correo VARCHAR(255),
    @Direccion VARCHAR(255),
    @Telefono VARCHAR(50),
    @Impuesto DECIMAL(5, 2)
AS
BEGIN
    UPDATE Proveedores
    SET Imagen = @Imagen,
        NumeroDocumento = @NumeroDocumento,
        Nombre = @Nombre,
        Correo = @Correo,
        Direccion = @Direccion,
        Telefono = @Telefono,
        Impuesto = @Impuesto
    WHERE IdProveedor = @IdProveedor;
END;




GO
/****** Object:  StoredProcedure [dbo].[ActualizarUsuario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarUsuario]
    @IdUsuario INT,
    @Identificacion VARCHAR(50),
	@Nombre VARCHAR(100),
    @Correo VARCHAR(100),
    @Contrasenna VARCHAR(100),
    @IdRol TINYINT,
    @Estado bit

AS
BEGIN
    UPDATE Usuario
    SET Identificacion = @Identificacion,
        Nombre = @Nombre,
        Correo = @Correo,
        Contrasenna = @Contrasenna,
        IdRol = @IdRol,
		Estado = @Estado
    WHERE IdUsuario = @IdUsuario;
END;



GO
/****** Object:  StoredProcedure [dbo].[AgregarProducto]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarProducto]
     @IdProducto INT,
	@Nombre NVARCHAR(100),
    @Precio DECIMAL(18,2),
    @IdCategoria INT,
    @Stock INT
AS
BEGIN
    INSERT INTO Producto (IdProducto, Nombre, Precio, IdCategoria, Stock)
    VALUES (@IdProducto, @Nombre, @Precio, @IdCategoria, @Stock)
END


GO
/****** Object:  StoredProcedure [dbo].[AgregarProductoAlCarrito]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarProductoAlCarrito]
    @IdProducto INT,
    @Cantidad INT
AS
BEGIN
    DECLARE @IdCarrito INT
    SET @IdCarrito = (SELECT TOP 1 IdCarrito FROM Carrito ORDER BY Fecha DESC)

    IF @IdCarrito IS NULL
    BEGIN
        INSERT INTO Carrito DEFAULT VALUES
        SET @IdCarrito = SCOPE_IDENTITY()
    END

    INSERT INTO CarritoDetalles (IdCarrito, IdProducto, Cantidad, Precio)
    VALUES (@IdCarrito, @IdProducto, @Cantidad, (SELECT Precio FROM Producto WHERE IdProducto = @IdProducto))
END

GO
/****** Object:  StoredProcedure [dbo].[ConsultarEmpleados]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarEmpleados]
AS
BEGIN
    SELECT IdEmpleado, Nombre, Apellido, FechaIngreso, HorasTrabajadas, HorasRebajadas, ValorPorHora, SalarioAjustado
    FROM dbo.Empleados;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarInventario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarInventario]
AS
BEGIN
    SELECT * FROM Inventario;
END;


GO
/****** Object:  StoredProcedure [dbo].[ConsultarProductos]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarProductos]
AS
BEGIN
    SELECT * FROM Producto
END


GO
/****** Object:  StoredProcedure [dbo].[ConsultarProveedores]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarProveedores]
AS
BEGIN
    SELECT * FROM Proveedores;
END;




GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarios]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarUsuarios]
AS
BEGIN
    SELECT IdUsuario,Identificacion,Nombre,Correo, U.IdRol,
	CASE WHEN Estado = 1 THEN 'Activo'
		ELSE 'Inactivo' END 'Estado', R.Descripcion
		FROM dbo.Usuario U
		INNER JOIN dbo.tRol  R on U.IdRol = R.IdRol;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarEmpleado]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarEmpleado]
    @IdEmpleado INT
AS
BEGIN
    -- Eliminando el empleado especificado por IdEmpleado
    DELETE FROM dbo.Empleados
    WHERE IdEmpleado = @IdEmpleado;
END;


GO
/****** Object:  StoredProcedure [dbo].[EliminarInventario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarInventario]
    @IdInventario INT
AS
BEGIN
    DELETE FROM Inventario WHERE ID = @IdInventario;
END;




GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProducto]
    @IdProducto INT
AS
BEGIN
    DELETE FROM Producto WHERE IdProducto = @IdProducto
END


GO
/****** Object:  StoredProcedure [dbo].[EliminarProveedor]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProveedor]
    @IdProveedor INT
AS
BEGIN
    DELETE FROM Proveedores WHERE IdProveedor = @IdProveedor;
END;




GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarUsuario]
    @IdUsuario INT
AS
BEGIN
    DELETE FROM Usuario WHERE IdUsuario = @IdUsuario;
END;


GO
/****** Object:  StoredProcedure [dbo].[LoginUsuario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LoginUsuario]
    @Correo varchar(100),
    @Contrasenna varchar(100)
AS
BEGIN
    SELECT IdUsuario,Identificacion, Nombre, Correo, U.IdRol, Estado, R.Descripcion
    FROM dbo.Usuario U
	INNER JOIN dbo.tRol R on U.IdRol = R.IdRol
    WHERE Correo = @Correo 
	AND Contrasenna = @Contrasenna 
	AND Estado = 1
END




GO
/****** Object:  StoredProcedure [dbo].[ObtenerEmpleadoPorID]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerEmpleadoPorID]
    @IdEmpleado INT
AS
BEGIN
    SELECT IdEmpleado, Nombre, Apellido, FechaIngreso, HorasTrabajadas, HorasRebajadas, ValorPorHora, SalarioAjustado
    FROM dbo.Empleados
    WHERE IdEmpleado = @IdEmpleado;
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerProductoPorId]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerProductoPorId]
    @IdProducto INT
AS
BEGIN
    SELECT IdProducto, Nombre, Precio
    FROM Producto
    WHERE IdProducto = @IdProducto;
END

GO
/****** Object:  StoredProcedure [dbo].[ObtenerProveedorPorID]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerProveedorPorID]
    @IdProveedor INT
AS
BEGIN
    SELECT * FROM Proveedores WHERE IdProveedor = @IdProveedor;
END;




GO
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorID]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUsuarioPorID]
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdUsuario, Identificacion, Nombre, Correo, Contrasenna, Estado, IdRol
    FROM dbo.Usuario
    WHERE IdUsuario = @IdUsuario;
END




GO
/****** Object:  StoredProcedure [dbo].[RegistrarEmpleado]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarEmpleado]
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaIngreso DATE,
    @HorasTrabajadas INT,
    @HorasRebajadas INT,
    @ValorPorHora DECIMAL(10, 2)
AS
BEGIN
    DECLARE @SalarioAjustado DECIMAL(10, 2);

    -- Calculando el salario ajustado
    SET @SalarioAjustado = (@HorasTrabajadas * @ValorPorHora) - (@HorasRebajadas * @ValorPorHora);

    INSERT INTO dbo.Empleados
        (Nombre, Apellido, FechaIngreso, HorasTrabajadas, HorasRebajadas, ValorPorHora, SalarioAjustado)
    VALUES
        (@Nombre, @Apellido, @FechaIngreso, @HorasTrabajadas, @HorasRebajadas, @ValorPorHora, @SalarioAjustado);
    
    -- Opcional: Devuelve el ID del empleado recién insertado
    SELECT SCOPE_IDENTITY() AS 'IdEmpleado';
END;

GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarProveedor]
    @Imagen VARCHAR(255),
    @NumeroDocumento VARCHAR(50),
    @Nombre VARCHAR(255),
    @Correo VARCHAR(255),
    @Direccion VARCHAR(255),
    @Telefono VARCHAR(50),
    @Impuesto DECIMAL(5, 2)
AS
BEGIN
    INSERT INTO Proveedores (Imagen, NumeroDocumento, Nombre, Correo, Direccion, Telefono, Impuesto)
    VALUES (@Imagen, @NumeroDocumento, @Nombre, @Correo, @Direccion, @Telefono, @Impuesto);
END;




GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarUsuario]
    @Identificacion varchar(50),
    @Nombre varchar(100),
    @Correo varchar(100),
    @Contrasenna varchar(100),
    @IdRol tinyint
AS
BEGIN
    DECLARE @Estado BIT = 1

    IF NOT EXISTS (SELECT 1 FROM dbo.Usuario WHERE Correo = @Correo OR Identificacion = @Identificacion)
    BEGIN
        INSERT INTO dbo.Usuario (Identificacion, Nombre, Correo, Contrasenna, IdRol, Estado)
        VALUES (@Identificacion, @Nombre, @Correo, @Contrasenna, @IdRol, @Estado)
    END
END


GO
/****** Object:  StoredProcedure [dbo].[VaciarCarrito]    Script Date: 6/30/2024 9:37:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VaciarCarrito]
AS
BEGIN
    DELETE FROM Carrito;
END

GO
