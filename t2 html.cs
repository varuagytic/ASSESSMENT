@model IEnumerable<UserManagement.Models.User>

<table class="table table-bordered table-striped">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.Forename)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Surname)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Email)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.IsActive)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.DateOfBirth)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    @Html.DisplayFor