USE [master]
GO
/****** Object:  Database [SuperMas]    Script Date: 12/08/2024 01:17:38 p. m. ******/
CREATE DATABASE [SuperMas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SuperMas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SuperMas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SuperMas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SuperMas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SuperMas] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SuperMas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SuperMas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SuperMas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SuperMas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SuperMas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SuperMas] SET ARITHABORT OFF 
GO
ALTER DATABASE [SuperMas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SuperMas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SuperMas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SuperMas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SuperMas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SuperMas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SuperMas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SuperMas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SuperMas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SuperMas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SuperMas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SuperMas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SuperMas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SuperMas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SuperMas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SuperMas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SuperMas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SuperMas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SuperMas] SET  MULTI_USER 
GO
ALTER DATABASE [SuperMas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SuperMas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SuperMas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SuperMas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SuperMas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SuperMas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SuperMas] SET QUERY_STORE = ON
GO
ALTER DATABASE [SuperMas] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SuperMas]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  Table [dbo].[DetalleFacturas]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFacturas](
	[IdDetalle] [int] IDENTITY(1,1) NOT NULL,
	[IdFactura] [int] NULL,
	[IdProducto] [nvarchar](50) NULL,
	[NombreProducto] [nvarchar](100) NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
	[Descuento] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[Cedula] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[FechaIngreso] [date] NULL,
	[HorasTrabajadas] [int] NULL,
	[HorasRebajadas] [int] NULL,
	[ValorPorHora] [decimal](10, 2) NULL,
	[SalarioAjustado] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
	[IdCajero] [int] NULL,
	[NombreCajero] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventario](
	[IdInventario] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[FechaIngreso] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdInventario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nominas]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nominas](
	[IdNomina] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [int] NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[Vacaciones] [int] NULL,
	[Horas] [int] NULL,
	[SalarioAjustado] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNomina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IdProducto] [nvarchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[Proveedores]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  Table [dbo].[tRol]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  Table [dbo].[Ventas]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
ALTER TABLE [dbo].[Inventario] ADD  DEFAULT (getdate()) FOR [FechaIngreso]
GO
ALTER TABLE [dbo].[Nominas]  WITH CHECK ADD  CONSTRAINT [FK_Nominas_Empleados] FOREIGN KEY([Cedula])
REFERENCES [dbo].[Empleados] ([Cedula])
GO
ALTER TABLE [dbo].[Nominas] CHECK CONSTRAINT [FK_Nominas_Empleados]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEmpleado]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEmpleado]
    @Cedula INT,
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
    WHERE Cedula = @Cedula;
END;


GO
/****** Object:  StoredProcedure [dbo].[ActualizarFactura]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarFactura]
    @IdFactura INT,
    @IdDetalle INT
AS
BEGIN
    DECLARE 
        @SubTotal DECIMAL(18, 2),
        @IVA DECIMAL(18, 2),
        @Total DECIMAL(18, 2);

    SET @SubTotal = (SELECT SUM(Total) FROM DetalleFacturas WHERE IdFactura = @IdFactura);

    
    SET @IVA = @SubTotal * 0.13;
    SET @Total = @SubTotal + @IVA;

    UPDATE Facturas
    SET Subtotal = @SubTotal,
        IVA = @IVA,
        Total = @Total
    WHERE IdFactura = @IdFactura;
END;


GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProducto]
    @IdProducto NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(18, 2),
    @Stock INT,
    @IdCategoria INT
AS
BEGIN
    UPDATE Productos
    SET Nombre = @Nombre,
        Precio = @Precio,
        IdCategoria = @IdCategoria,
        Stock = @Stock
    WHERE IdProducto = @IdProducto
END


GO
/****** Object:  StoredProcedure [dbo].[ActualizarProveedor]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ActualizarUsuario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarEmpleados]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarEmpleados]
AS
BEGIN
    SELECT Cedula, Nombre, Apellido, FechaIngreso, HorasTrabajadas, HorasRebajadas, ValorPorHora, SalarioAjustado
    FROM dbo.Empleados;
END;


GO
/****** Object:  StoredProcedure [dbo].[ConsultarFacturasPorFecha]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ConsultarFacturasPorFecha]
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [IdFactura]
          ,[Fecha]
          ,[Subtotal]
          ,[IVA]
          ,[Total]
          ,[IdCajero]
          ,[NombreCajero]
    FROM [SuperMas].[dbo].[Facturas]
    WHERE Fecha >= @FechaInicio AND Fecha <= @FechaFin;
END
GO
/****** Object:  StoredProcedure [dbo].[Consultarinventario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Consultarinventario]
AS
BEGIN
    SELECT * FROM Inventario
END


GO
/****** Object:  StoredProcedure [dbo].[ConsultarProductos]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConsultarProductos]
AS
BEGIN
    SELECT * FROM Productos
END


GO
/****** Object:  StoredProcedure [dbo].[ConsultarProveedores]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarios]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
		INNER JOIN dbo.tRol  R on U.IdRol = R.IdRol;
END;




GO
/****** Object:  StoredProcedure [dbo].[CrearNomina]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearNomina]
    @Cedula NVARCHAR(50),
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Vacaciones INT,
    @Horas INT,
    @IdNomina INT OUTPUT
AS
BEGIN
    DECLARE @SalarioAjustado DECIMAL(10, 2);

    -- Verificar si el empleado existe y obtener el SalarioAjustado
    IF EXISTS (SELECT 1 FROM Empleados WHERE Cedula = @Cedula)
    BEGIN
        SELECT @SalarioAjustado = SalarioAjustado
        FROM Empleados
        WHERE Cedula = @Cedula;

        -- Insertar en la tabla Nominas
        INSERT INTO Nominas (Cedula, Nombre, Apellido, Vacaciones, Horas, SalarioAjustado)
        VALUES (@Cedula, @Nombre, @Apellido, @Vacaciones, @Horas, @SalarioAjustado);

        SET @IdNomina = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- Manejar el caso en que el Cedula no existe
        RAISERROR ('La Cedula especificada no existe.', 16, 1);
        RETURN;
    END
END;


GO
/****** Object:  StoredProcedure [dbo].[EliminarEmpleado]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarEmpleado]
    @Cedula INT
AS
BEGIN
    -- Eliminando el empleado especificado por Cedula
    DELETE FROM dbo.Empleados
    WHERE Cedula = @Cedula;
END;


GO
/****** Object:  StoredProcedure [dbo].[EliminarInventario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[EliminarInventario]
    @IdInventario INT
AS
BEGIN
    DELETE FROM Inventario WHERE IdInventario = @IdInventario;
END;



GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProducto]
	@IdProducto NVARCHAR(50)

AS
BEGIN
    DELETE FROM Productos WHERE IdProducto = @IdProducto
END

GO
/****** Object:  StoredProcedure [dbo].[EliminarProveedor]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[LoginUsuario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ObtenerEmpleadoPorCedula]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[ObtenerEmpleadoPorCedula]
    @Cedula NVARCHAR(50)
AS
BEGIN
    SELECT Cedula, Nombre, Apellido, FechaIngreso, HorasTrabajadas, HorasRebajadas, ValorPorHora, SalarioAjustado
    FROM dbo.Empleados
    WHERE Cedula = @Cedula;
END;


GO
/****** Object:  StoredProcedure [dbo].[ObtenerNominas]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerNominas]
AS
BEGIN
    SELECT 
        IdNomina,
        Cedula,
        Nombre,
        Apellido,
        Vacaciones,
        Horas,
        SalarioAjustado
    FROM 
        Nominas
END


GO
/****** Object:  StoredProcedure [dbo].[ObtenerProductoPorId]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerProductoPorId]
    @IdProducto NVARCHAR(50)
AS
BEGIN
    SELECT IdProducto, Nombre, Precio, Stock, IdCategoria
    FROM Productos
    WHERE IdProducto = @IdProducto
END


GO
/****** Object:  StoredProcedure [dbo].[ObtenerProveedorPorID]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[ObtenerUsuarioPorID]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarDetalleFactura]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarDetalleFactura]
	@IdProducto nvarchar(50),
    @Cantidad INT,
	@IdCajero INT,
	@NuevaFactura INT,
	@Descuento INT

AS
BEGIN
	IF @NuevaFactura = 1
		BEGIN
			EXEC dbo.RegistrarFactura @IdCajero = @IdCajero
		END
	
    DECLARE 
	    @IdFactura INT,
		@Fecha datetime,
		@NombreProducto nvarchar(100),
		@PrecioUnitario decimal(10,2),
		@SubTotal decimal(10,2),
		@Total decimal(10,2)
		

	
	SET 
		@IdFactura = (SELECT top(1) IdFactura from Facturas order by IdFactura desc)
	SET
		@NombreProducto = (Select Nombre from Productos where IdProducto = @IdProducto)
	SET
		@PrecioUnitario = (Select Precio from Productos where IdProducto = @IdProducto)
	SET
		@SubTotal = (@PrecioUnitario * @Cantidad) 
	SET
		@Total = (@SubTotal - ((@SubTotal * @Descuento) / 100))

	INSERT INTO dbo.DetalleFacturas(IdFactura, IdProducto, NombreProducto, Cantidad, PrecioUnitario, Total, Descuento)
	VALUES(@IdFactura, @IdProducto, @NombreProducto, @Cantidad, @PrecioUnitario, @Total,@Descuento) 

	DECLARE
		@DetalleId INT
	SET
		@DetalleId = (SELECT top(1) IdDetalle from DetalleFacturas order by IdDetalle desc)


	EXEC dbo.ActualizarFactura @IdFactura = @IdFactura, @IdDetalle = @DetalleId

END;



GO
/****** Object:  StoredProcedure [dbo].[RegistrarEmpleado]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarEmpleado]
    @Cedula INT,
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

    INSERT INTO Empleados (Cedula, Nombre, Apellido, FechaIngreso, HorasTrabajadas, HorasRebajadas, ValorPorHora, SalarioAjustado)
    VALUES (@Cedula, @Nombre, @Apellido, @FechaIngreso, @HorasTrabajadas, @HorasRebajadas, @ValorPorHora, @SalarioAjustado);
END;


GO
/****** Object:  StoredProcedure [dbo].[RegistrarFactura]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarFactura]
	@IdCajero INT

AS
BEGIN
    DECLARE 
		@Fecha datetime,
		@NombreCajero nvarchar(50)

    SET 
		@Fecha = CAST(GETDATE() AS date)
	SET
		@NombreCajero = (SELECT Nombre From Empleados Where Cedula = @IdCajero)

	INSERT INTO dbo.Facturas(Fecha,IdCajero,NombreCajero)
	VALUES(@Fecha,@IdCajero,@NombreCajero) 

END;


GO
/****** Object:  StoredProcedure [dbo].[RegistrarProducto]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarProducto]
    @IdProducto NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @Precio DECIMAL(18, 2),
    @Stock INT,
    @IdCategoria INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Productos (IdProducto, Nombre, Precio, Stock, IdCategoria)
    VALUES (@IdProducto, @Nombre, @Precio, @Stock, @IdCategoria);
END

GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[RegistrarUsuario]    Script Date: 12/08/2024 01:17:38 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[VaciarCarrito]    Script Date: 12/08/2024 01:17:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VaciarCarrito]
AS
BEGIN
    DELETE FROM Carrito;
END




GO
USE [master]
GO
ALTER DATABASE [SuperMas] SET  READ_WRITE 
GO
