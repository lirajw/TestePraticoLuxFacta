# Enquete
	Requisitos Minimos:
	Sql Server 2014
	.Net Framework 4.6.1
	
	Configurações Iniciais:
	 * Para criar a base de dados, execute o arquivo scriptCreateLuxFacta.sql em uma instancia aberta do SQL Server de sua maquina.
	 * No arquivo Web.Config,na TAG <connectionStrings> / name="LuxFacta", altere a connectionString para uma compativel com a instancia do seu SQL Server.
		EX:
		  <connectionStrings>
			<add name="LuxFacta" connectionString="Digite Aqui sua String de COnexao"/>
		  </connectionStrings>
		  
		  ATENÇÃO : Não altere o nome da conexao! (name="LuxFacta)
