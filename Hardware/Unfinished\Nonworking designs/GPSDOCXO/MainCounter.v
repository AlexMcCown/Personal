`timescale 1ns/1ps
module MainCounter (PPS, CLK, CLR, RUN, MULT, DIFF, RDY);

parameter TF = 10_000_000; //our target frequency

input PPS;
input CLK;
input CLR;
input RUN;
input [7:0] MULT;

output reg [15:0] DIFF;
output reg RDY = 1'b0;

reg[31:0] clkCounter1 = 0;
reg[31:0] clkCounter2 = 0;
reg[9:0] ppsCtr = 0; //we start at 0 as the first rising edge contains no useful counter information 
reg clrClkCtr = 0;
reg clkCtrSel = 1;
reg enblRun = 0;


always @(posedge PPS) begin
	if(RUN == 1'b1 && enblRun == 1)begin
		if(CLR == 1'b0) begin
			if(ppsCtr < (MULT + 1)) begin
				ppsCtr = ppsCtr + 1;
				RDY = 1'b0;
			end
			else begin
				if(clkCtrSel == 0) begin
					DIFF = (clkCounter1 - (TF * (MULT + 1) ) + 2147483648)+32767;
					/*
					 * DIFF is equal to our counter, minus the result of multiplying our expected value (10MHz) 
					 * with 1 plus our multiplier (the multiplier is 1 less its intended value), offsetting that value
					 * by half of 32 bits and adding 1 less than half of 16 bits to give our difference value 
					*/
					$display ("Clk Ctr 1 = %d", (clkCounter1 - (TF * (MULT + 1) ))-1);
					clkCtrSel = 1;
					RDY = 1'b1;
				end
				else begin
					DIFF = (clkCounter2 - (TF * (MULT + 1) ) + 2147483648)+32767;
					$display ("Clk Ctr 2 = %d", (clkCounter2 - (TF * (MULT + 1) ))-1);
					clkCtrSel = 0;
					RDY = 1'b1;
				end
				clrClkCtr = 1;
				ppsCtr = 1;
			end
		end
		else begin
			clrClkCtr = 1;
			ppsCtr <= 1;
			enblRun = 0;
			
		end
	end
	else
		enblRun = 1; //we ignore the first pulse and dont count until the second pulse to make sure everything is lined up at start
end



always @(posedge CLK) begin
	if(RUN == 1'b1 && enblRun == 1)begin
		if(CLR == 1'b0) begin
			if(clkCtrSel == 0) begin
				clkCounter2 = 1;
				clkCounter1 = clkCounter1 + 1;
			end
			else begin
				clkCounter1 = 1;
				clkCounter2 = clkCounter2 + 1;
			end
		end
		else begin
			
		end
	end
end





endmodule