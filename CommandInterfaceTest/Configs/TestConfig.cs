namespace CommandInterface.Configs.Test
{
    class TestCommandSetConfig : CommandSetConfig
    {
        public TestCommandSetConfig()
        {
            this.Name = "TestCommandSet";
            this.ScriptDirectory = "TestDirectory";
            this.ProjectDirecotry = "TestProjectDirectory";
        }
    }
}
