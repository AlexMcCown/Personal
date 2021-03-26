module ShiftIn (CLK, SI, RST, LATCH, PO); 
input CLK;
input SI; 
input LATCH;
input RST;
output reg [7:0] PO; 
reg [7:0] tmp; 
 
  always @(posedge CLK)begin 
	if(RST == 1'b0)
		tmp = {tmp[6:0], SI}; 
	else
		tmp = 8'h00;
  end 
	always@(posedge LATCH)begin
		PO = tmp;
	end
endmodule