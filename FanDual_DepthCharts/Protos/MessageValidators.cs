namespace FanDual_DepthCharts.Protos;

public static class MessageValidators
{
    public static ArgumentException? ValidateRequest(this RequestAddPlayerToDepthChart message)
    {
        // Your logic goes here. In a real scenario, it would be a more complex operation.
        if (message.TeamId == 0)
        {
            return new ArgumentException("team id should be defined");
        }

        if (message.SportId == 0)
        {
            return new ArgumentException("sport id should be defined");
        }

        if (message.PositionCode == "")
        {
            return new ArgumentException("position code should be defined");
        }

        return null;
    }

    public static ArgumentException? ValidateRequest(this RequestRemovePlayerFromDepthChart message)
    {
        if (message.PositionCode == "")
        {
            throw new ArgumentException("position is not defined for request");
        }

        if (message.SportId == 0)
        {
            throw new ArgumentException("sport is not defined for request");
        }

        if (message.TeamId == 0)
        {
            throw new ArgumentException("team is not defined for request");
        }

        return null;
    }


    public static ArgumentException? ValidateRequest(this RequestGetFullDepthChart message)
    {
        if (message.SportId == 0)
        {
            throw new ArgumentException("sport is not defined for request");
        }

        if (message.TeamId == 0)
        {
            throw new ArgumentException("team is not defined for request");
        }

        return null;
    }
}