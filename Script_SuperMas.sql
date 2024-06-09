CREATE DATABASE SuperMas;
GO
USE [SuperMas]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[FechaIngreso] [date] NOT NULL,
	[Salario] [decimal](10, 2) NOT NULL,
	[DiasTrabajados] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VentasID] [int] NULL,
	[TotalAPagar] [decimal](10, 2) NOT NULL,
	[MontoCancelado] [decimal](10, 2) NOT NULL,
	[Cambio] [decimal](10, 2) NULL,
	[TipoDePago] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductoID] [int] NULL,
	[Cantidad] [int] NOT NULL,
	[ProveedorID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[CategoriaID] [int] NULL,
	[Precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoVenta]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoVenta](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductoID] [int] NULL,
	[VentaID] [int] NULL,
	[Cantidad] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  Table [dbo].[tRol]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Consecutivo] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](100) NOT NULL,
	[IdRol] [tinyint] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Cedula] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ventas]    Script Date: 6/8/2024 7:45:34 PM ******/
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
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([VentasID])
REFERENCES [dbo].[Ventas] ([ID])
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([VentasID])
REFERENCES [dbo].[Ventas] ([ID])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Producto] ([ID])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Producto] ([ID])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([IdProveedor])
GO
ALTER TABLE [dbo].[Inventario]  WITH CHECK ADD FOREIGN KEY([ProveedorID])
REFERENCES [dbo].[Proveedores] ([IdProveedor])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([CategoriaID])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[ProductoVenta]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Producto] ([ID])
GO
ALTER TABLE [dbo].[ProductoVenta]  WITH CHECK ADD FOREIGN KEY([ProductoID])
REFERENCES [dbo].[Producto] ([ID])
GO
ALTER TABLE [dbo].[ProductoVenta]  WITH CHECK ADD FOREIGN KEY([VentaID])
REFERENCES [dbo].[Ventas] ([ID])
GO
ALTER TABLE [dbo].[ProductoVenta]  WITH CHECK ADD FOREIGN KEY([VentaID])
REFERENCES [dbo].[Ventas] ([ID])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_tRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[tRol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_tRol]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProveedor]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarProveedores]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  StoredProcedure [dbo].[EliminarProveedor]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  StoredProcedure [dbo].[LoginUsuario]    Script Date: 6/8/2024 7:45:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[LoginUsuario]
    @Correo varchar(100),
    @Contrasenna varchar(100)
AS
BEGIN
    SELECT Consecutivo,Identificacion, Nombre, Correo, U.IdRol, Estado, R.Descripcion
    FROM dbo.Usuario U
	INNER JOIN dbo.tRol R on U.IdRol = R.IdRol
    WHERE Correo = @Correo 
	AND Contrasenna = @Contrasenna 
	AND Estado = 1
END


GO
/****** Object:  StoredProcedure [dbo].[ObtenerProveedorPorID]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 6/8/2024 7:45:34 PM ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 6/8/2024 7:45:34 PM ******/
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
